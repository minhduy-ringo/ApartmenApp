using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using StaffManagement.Pages.Manager;
using StaffManagement.Pages.Staff;
using System.Net.Http;
using Newtonsoft.Json;
using StaffManagement.Model;

namespace StaffManagement
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        async void Login_Clicked(object sender, EventArgs e)
        {
            var pw = password.Text;
            var userName = username.Text;
            using (var client = new HttpClient())
            {
                var ip = Application.Current.Properties["ip"];
                OData_Staff oData;
                try
                {
                    var json = await client.GetStringAsync(ip + "/StaffApi/odata/Login?user=" + userName + "&pass=" + pw);
                    oData = JsonConvert.DeserializeObject<OData_Staff>(json);
                    if (oData.StaffList.First<Staff>().isManager == false)
                    {
                        var staffPage = new StaffMainPage();
                        staffPage.BindingContext = oData.StaffList.First<Staff>();
                        await Navigation.PushAsync(staffPage);
                    }
                    else
                    {
                        var managerPage = new ManagerPage();
                        managerPage.BindingContext = oData.StaffList.First<Staff>();
                        await Navigation.PushAsync(managerPage);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    await DisplayAlert("Lỗi", ex.Message, "Xác nhận");
                }
            }
        }
    }
}
