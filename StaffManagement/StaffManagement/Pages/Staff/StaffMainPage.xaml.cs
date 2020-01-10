using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StaffManagement.Pages.Staff
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StaffMainPage : MasterDetailPage
    {
        public StaffMainPage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as StaffMainPageMasterMenuItem;
            if (item != null)
            {
                Detail = new NavigationPage((Xamarin.Forms.Page)Activator.CreateInstance(item.TargetType));
                MasterPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}