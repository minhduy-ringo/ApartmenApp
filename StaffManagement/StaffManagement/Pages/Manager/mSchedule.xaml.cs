using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StaffManagement.Model;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Converters;

namespace StaffManagement.Pages.Manager
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class mSchedule : ContentPage
    {
        public mSchedule()
        {
            InitializeComponent();
            LoadData();
        }
        public async void LoadData()
        {
            using (var client = new HttpClient())
            {
                var ip = Application.Current.Properties["ip"];
                var json = await client.GetStringAsync(ip + "/StaffApi/odata/ManagerScheduleViews");
                var odata = JsonConvert.DeserializeObject<StaffManagement.Model.Odata_ScheduleView>(json);
                ScheduleViewList.ItemsSource = odata.ScheduleViewList;
            }
        }
    }
}