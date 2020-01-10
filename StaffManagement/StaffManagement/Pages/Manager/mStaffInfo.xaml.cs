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
using StaffManagement.Pages;
//http://172.29.64.131:4420

namespace StaffManagement.Pages.Manager
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class mStaffInfo : ContentPage
    {
        private OData_Staff oData;

        public mStaffInfo()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            LoadData();
        }
        public async void LoadData()
        {
            using (var client = new HttpClient())
            {
                // sv ip: 172.29.64.131
                var ip = Application.Current.Properties["ip"];
                var json = await client.GetStringAsync(ip + "/StaffApi/odata/Staffs");
                this.oData = JsonConvert.DeserializeObject<OData_Staff>(json);
                StaffList.ItemsSource = this.oData.StaffList;
            }
        }
        //async void OnAddButtonClicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new mAddNewStaff
        //    {
        //        BindingContext = new mAddNewStaff()
        //    });
        //}
        private void OnTextChanged (object sender, TextChangedEventArgs e)
        {
            StaffList.BeginRefresh();
            if (string.IsNullOrEmpty(e.NewTextValue))
                StaffList.ItemsSource = this.oData.StaffList;
            else
            {
                StaffList.ItemsSource = this.oData.StaffList.Where(i => i.name.ToLower().Contains(e.NewTextValue.ToLower()));
            }
            StaffList.EndRefresh();
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