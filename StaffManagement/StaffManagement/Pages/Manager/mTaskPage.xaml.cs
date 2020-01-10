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
        private Model.Odata_Task oData = new Odata_Task();
        public mTaskPage()
        {
            InitializeComponent();
            PoppulatePicker();
        }
        private void PoppulatePicker()
        {
            var typeList = new List<string>();
            typeList.Add("Sửa chữa");   // code 1
            typeList.Add("Bảo trì");    // code 2
            typeList.Add("Vận chuyển"); // code 3
            typeList.Add("Khác");       // code 4
            this.type_picker.ItemsSource = typeList;

            var statusList = new List<string>();
            statusList.Add("Tiếp nhận");    //code 1
            statusList.Add("Tiến hành");    //code 2
            statusList.Add("Hoàn thành");   //code 3
            this.status_picker.ItemsSource = statusList;
        }
        private async void LoadData()
        {
            using (var client = new HttpClient())
            {
                var type = type_picker.SelectedItem;
                var ip = Application.Current.Properties["ip"];
                var json = await client.GetStringAsync(ip + "/TaskApi/odata/Tasks");
                this.oData = JsonConvert.DeserializeObject<Odata_Task>(json);
            }
        }
        public void OnTaskPickerChanged(object sender, SelectedItemChangedEventArgs e)
        {
            
        }
        private void ViewCell_Tapped(object sender, EventArgs e)
        {

        }
        protected override void OnAppearing()
        {
            LoadData();
        }
    }
}