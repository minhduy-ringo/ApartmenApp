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

namespace StaffManagement.Page.Manager
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class mTaskPage : ContentPage
    {
        ObservableCollection<TaskView> TaskViews = new ObservableCollection<TaskView>();
        public mTaskPage()
        {
            InitializeComponent();
            LoadTaskTypes();
        }
        public async void LoadTaskTypes()
        {
            using(var client = new HttpClient())
            {
                var ip = Application.Current.Properties["ip"];
                var json = await client.GetStringAsync(ip + "/Task/odata/TaskTypes");
                var odata = JsonConvert.DeserializeObject<Odata_TaskType>(json);
                type_picker.ItemsSource = odata.TaskTypeList;
            }
        }
        public async void OnTaskTypeChanged(object sender, EventArgs e)
        {
            using(var client = new HttpClient())
            {
                var type = type_picker.SelectedItem as TaskType;
                var ip = Application.Current.Properties["ip"];
                var json = await client.GetStringAsync(ip + "/Task/odata/ManagerTaskViews?$filter=substringof('" + type.taskTypeName.ToString() + "',taskTypeName)");
                var odata = JsonConvert.DeserializeObject<StaffManagement.Model.Odata_TaskView>(json);
                TaskViewList.ItemsSource = null;
                TaskViewList.ItemsSource = odata.TaskViewList;
            }
        }
        private void ViewCell_Tapped(object sender, EventArgs e)
        {

        }
    }
}