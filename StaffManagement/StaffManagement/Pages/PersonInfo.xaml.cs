using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StaffManagement.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonInfo : ContentPage
    {
        public PersonInfo()
        {
            InitializeComponent();
        }
        private void ChangeInfoView(object sender, EventArgs e)
        {
            
        }
        private void ChangeTaskView(object sender, EventArgs e)
        {
            //info.Content = new ContentView();
        }
    }
}