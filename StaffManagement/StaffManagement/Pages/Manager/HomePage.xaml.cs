using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StaffManagement.Pages.Manager;
using StaffManagement.Model;

namespace StaffManagement.Pages.Manager
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }
        private async void OnTaskTapped(object sender, EventArgs e)
        {
            var taskPage = new mTaskPage();
            await Navigation.PushAsync(taskPage);
        }
        private async void OnLeaveTapped(object sender, EventArgs te)
        {
            var leavePage = new mLeavePage();
            await Navigation.PushAsync(leavePage);
        }
        private async void OnAvatarTapped(object sender, EventArgs te)
        {
            var infoPage = new PersonInfo();
            infoPage.BindingContext = this.BindingContext;
            await Navigation.PushAsync(infoPage);
        }
        private async void OnNoticeTapped(object sender, EventArgs te)
        {
            var noticePage = new mNoticePage();
            await Navigation.PushAsync(noticePage);
        }
    }
}