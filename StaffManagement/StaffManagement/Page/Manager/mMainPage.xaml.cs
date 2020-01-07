using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StaffManagement.Page.Manager;

namespace StaffManagement.Page.Manager
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class mMainPage : ContentPage
    {
        public mMainPage()
        {
            InitializeComponent();
        }

        async void OnEmployeeTapped(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new mStaffInfo
            {
                BindingContext = new mStaffInfo()
            });
        }

        async void OnTaskTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new mTaskPage
            {
                BindingContext = new mTaskPage()
            });
        }

        async void OnScheduleTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new mSchedule
            {
                BindingContext = new mSchedule()
            });
        }
    }
}