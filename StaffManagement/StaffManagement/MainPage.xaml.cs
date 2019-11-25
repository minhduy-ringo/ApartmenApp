using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using StaffManagement.Page.Manager;
using StaffManagement.Page.Staff;

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
            if (userName == "manager" && pw == "123456")
            {
                await Navigation.PushAsync(new mMainPage
                {
                    BindingContext = new mMainPage()
                });
            }  
            if(userName == "staff" && pw == "123456")
            {
                await Navigation.PushAsync(new sMainPage
                {
                    BindingContext = new sMainPage()
                });
            }
        }
    }
}
