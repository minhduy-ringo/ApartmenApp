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
        public short taskTypeId { get; set; }
        public string taskStatus { get; set; }
        public Nullable<short> priority { get; set; }
        public Nullable<short> complexId { get; set; }
        public Nullable<short> buildingNum { get; set; }
        public Nullable<System.DateTime> startDate { get; set; }
        public Nullable<System.DateTime> endDate { get; set; }
        public string description { get; set; }
        public Nullable<decimal> cost { get; set; }
    }
    class Odata_Task
    {
        [JsonProperty("odata.metadata")]
        public string Metadata { get; set; }
        [JsonProperty("value")]
        public List<Staff> TaskList { get; set; }
    }

    class TaskView
    {
        public int taskId { get; set; }
        public string taskTypeName { get; set; }
        public string taskStatus { get; set; }
        public Nullable<short> buildingNum { get; set; }
    }

    class Odata_TaskView
    {
        [JsonProperty("odata.metadata")]
        public string Metadata { get; set; }
        [JsonProperty("value")]
        public List<TaskView> TaskViewList { get; set; }
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
