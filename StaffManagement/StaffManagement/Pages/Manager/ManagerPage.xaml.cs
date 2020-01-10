using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StaffManagement.Pages.Manager
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManagerPage : TabbedPage
    {
        public ManagerPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (await DisplayAlert("Đăng xuất", "", "Đồng ý", "Quay lại"))
                {
                    base.OnBackButtonPressed();
                    await Navigation.PopAsync();
                }
            });
            return true;
        }
    }
}