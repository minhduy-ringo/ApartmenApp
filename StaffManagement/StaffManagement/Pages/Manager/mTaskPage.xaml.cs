using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using StaffManagement.Model;
using System.Net.Http;
using System.Collections.ObjectModel;

namespace StaffManagement.Pages.Manager
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class mTaskPage : ContentPage
    {
        private Model.Odata_Task oData;

        public mTaskPage()
        {
            InitializeComponent();
            PoppulatePicker();
        }
        protected override void OnAppearing()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await LoadData();
                status_picker.SelectedIndex = -1;
                type_picker.SelectedIndex = -1;
            });
        }
        private void PoppulatePicker()
        {
            var typeList = new List<string>();
            typeList.Add("Toàn bộ");
            typeList.Add("Sửa chữa");   // code 1
            typeList.Add("Bảo trì");    // code 2
            typeList.Add("Vận chuyển"); // code 3
            typeList.Add("Khác");       // code 4
            this.type_picker.ItemsSource = typeList;

            var statusList = new List<string>();
            statusList.Add("Toàn bộ");
            statusList.Add("Tiếp nhận");    //code 1
            statusList.Add("Tiến hành");    //code 2
            statusList.Add("Chờ xác nhận"); //code 3
            statusList.Add("Hoàn thành");   //code 4
            this.status_picker.ItemsSource = statusList;
        }
        private async System.Threading.Tasks.Task LoadData()
        {
            using (var client = new HttpClient())
            {
                int complex = ((Model.Staff)this.BindingContext).complexId;
                var ip = Application.Current.Properties["ip"];
                var json = await client.GetStringAsync(ip + "/TaskApi/odata/Tasks/Tasks?complex=" + complex);
                this.oData = JsonConvert.DeserializeObject<Odata_Task>(json);
                TaskViewList.ItemsSource = this.oData.TaskList;
            }
        }
        public void OnPickerIndexChanged()
        {
            var type = type_picker.SelectedIndex;
            var status = status_picker.SelectedIndex;

            if (type == -1)
                type++;
            if (status == -1)
                status++;

            TaskViewList.BeginRefresh();
            if (type == 0 && status != 0)
            {
                TaskViewList.ItemsSource = this.oData.TaskList.Where(task => task.taskStatus == status);
            }
            if (status == 0 && type != 0)
            {
                TaskViewList.ItemsSource = this.oData.TaskList.Where(task => task.taskType == type);
            }
            if (status == 0 && type == 0)
            {
                TaskViewList.ItemsSource = this.oData.TaskList;
            }
            if (status != 0 && type != 0)
            {
                TaskViewList.ItemsSource = this.oData.TaskList.Where(task => task.taskType == type && task.taskStatus == status);
            }

            TaskViewList.EndRefresh();
        }
        private void type_picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnPickerIndexChanged();
        }

        private void status_picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnPickerIndexChanged();
        }

        private async void TaskViewList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new DetailTaskPage
            {
                BindingContext = e.SelectedItem as Model.Task
            });
        }
    }
}