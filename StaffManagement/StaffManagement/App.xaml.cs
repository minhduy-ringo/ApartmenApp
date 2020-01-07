using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace StaffManagement
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            Application.Current.Properties["ip"] = "http://192.168.43.24:8055";
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
