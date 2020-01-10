using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace StaffManagement.Model
{
    class Task
    {
        public int taskId { get; set; }
        public short taskType { get; set; }
        public short taskStatus { get; set; }
        public short priority { get; set; }
        public short complexId { get; set; }
        public short buildingId { get; set; }
        public System.DateTime startDate { get; set; }
        public Nullable<System.DateTime> endDate { get; set; }
        public string description { get; set; }
        public decimal cost { get; set; }
    }
    class Odata_Task
    {
        [JsonProperty("odata.metadata")]
        public string Metadata { get; set; }
        [JsonProperty("value")]
        public List<Staff> TaskList { get; set; }
    }
    public class TaskStatusConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(string.Equals(value,"1"))
            {
                var status = "Chờ xác nhận";
                return status;
            }
            if(string.Equals(value, "2"))
            {
                var status = "Đang thực hiện";
                return status;
            }
            if(string.Equals(value, "3"))
            {
                var status = "Hoàn thành";
                return status;
            }
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
