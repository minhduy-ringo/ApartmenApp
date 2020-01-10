using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StaffManagement.Pages.Staff
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StaffMainPageMaster : ContentPage
    {
        public ListView ListView;

        public StaffMainPageMaster()
        {
            InitializeComponent();

            BindingContext = new StaffMainPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class StaffMainPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<StaffMainPageMasterMenuItem> MenuItems { get; set; }

            public StaffMainPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<StaffMainPageMasterMenuItem>(new[]
                {
                    new StaffMainPageMasterMenuItem { Id = 0, Title = "Page 1" },
                    new StaffMainPageMasterMenuItem { Id = 1, Title = "Page 2" },
                    new StaffMainPageMasterMenuItem { Id = 2, Title = "Page 3" },
                    new StaffMainPageMasterMenuItem { Id = 3, Title = "Page 4" },
                    new StaffMainPageMasterMenuItem { Id = 4, Title = "Page 5" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}