using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StaffManagement.Page.Manager;
using System.Net.Http;
using Newtonsoft.Json;
using StaffManagement.Model;
using System.Net.Http.Headers;

namespace StaffManagement.Page.Manager
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class mAddNewStaff : ContentPage
    {
        public mAddNewStaff()
        {
            InitializeComponent();
            LoadDepartment();
        }
        public async void LoadDepartment()
        {
            using (var client = new HttpClient())
            {
                // sv ip: 172.29.64.131
                var ip = Application.Current.Properties["ip"];
                var json = await client.GetStringAsync(ip + "/Staff/odata/Departments");
                var odata = JsonConvert.DeserializeObject<OData_Department>(json);
                department_list.ItemsSource = odata.DepartmentsList;
            }
        }
        public async void OnAddButtonClicked(object sender, EventArgs e)
        {
            Model.Staff newStaff = new Model.Staff
            {
                name = name_entry.Text,
                birthday = birthday_pick.Date,
                address = address_entry.Text,
                city = city_entry.Text,
                mobile = mobile_entry.Text,
                departmentId = Convert.ToInt16(department_list.SelectedIndex),
                complexId = 1,
                joinDate = System.DateTime.Today,
            };
            bool answer = await DisplayAlert("Xác nhận", "Xác nhận thêm nhân viên mới", "Đồng ý", "Thoát");
            if(answer == true)
            {
                using (var client = new HttpClient())
                {
                    // sv ip: 172.29.64.131
                    var json = JsonConvert.SerializeObject(newStaff);
                    HttpContent httpContent = new StringContent(json);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
                    try
                    {
                        var ip = Application.Current.Properties["ip"];
                        var response = await client.PostAsync(ip + "/Staff/odata/Staffs", httpContent);
                        Debug.WriteLine(response.StatusCode);
                    }
                    catch (HttpRequestException hex)
                    {
                        
                    }
                }
            }
        }
    }
}