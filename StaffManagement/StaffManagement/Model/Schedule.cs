using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace StaffManagement.Model
{
    class ScheduleView
    {
        public int scheduleId { get; set; }
        public int staffId { get; set; }
        public System.DateTime wordDate { get; set; }
        public System.TimeSpan startWorkHour { get; set; }
        public System.TimeSpan endWorkHour { get; set; }
        public bool isHoliday { get; set; }
        public bool isWeekend { get; set; }
    }
    class Odata_ScheduleView
    {
        [JsonProperty("odata.metadata")]
        public string Metadata { get; set; }
        [JsonProperty("value")]
        public List<ScheduleView> ScheduleViewList { get; set; }
    }
}
