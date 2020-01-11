using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StaffManagement.Model;
using Newtonsoft.Json;
using System.Net.Http;

namespace StaffManagement.Pages.Manager.TaskDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Overview : ContentPage
    {
        public Overview()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var status = ((Model.Task)this.BindingContext).taskStatus;
            if(status == 3)
            {
                if (await DisplayAlert("Xác nhận", "Xác nhận hoàn thành công việc", "Đồng ý", "Quay lại"))
                {
                    var context = ((Model.Task)this.BindingContext);
                    context.taskStatus = 4;
                    using (var client = new HttpClient())
                    {
                        var ip = Application.Current.Properties["ip"];
                        try
                        {
                            string contentType = "application/json";
                            var serialize = JsonConvert.SerializeObject(context);
                            var content = new StringContent(serialize, Encoding.UTF8, contentType);
                            var result = await client.PutAsync(ip + "/TaskApi/odata/Tasks(" + context.taskId + ")", content);
                            ConfirmButton.Text = "Hoàn thành";

                        }
                        catch (Exception ex)
                        {
                            await DisplayAlert("Lỗi", ex.Message, "OK");
                        }
                    }
                }
            }
        }
    }
}