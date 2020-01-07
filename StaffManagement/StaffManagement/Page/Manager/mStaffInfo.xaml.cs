using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using StaffManagement.Model;
using StaffManagement.Page;
//http://172.29.64.131:4420

namespace StaffManagement.Page.Manager
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class mStaffInfo : ContentPage
    {

        public mStaffInfo()
        {
            InitializeComponent();
            LoadData();
        }
        
        public async void LoadData()
        {
            using (var client = new HttpClient())
            {
                // sv ip: 172.29.64.131
                var ip = Application.Current.Properties["ip"];
                var json = await client.GetStringAsync(ip + "/StaffApi/odata/Staffs");
                var odata = JsonConvert.DeserializeObject<OData_Staff>(json);
                StaffList.ItemsSource = odata.StaffList;
            }
        }
        async void OnAddButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new mAddNewStaff
            {
                BindingContext = new mAddNewStaff()
            });
        }
        void OnTextChanged (object sender, EventArgs e)
        {
        }
        private async void StaffList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new PersonInfo
            {
                BindingContext = e.SelectedItem as Model.Staff
            });
        }
    }
}