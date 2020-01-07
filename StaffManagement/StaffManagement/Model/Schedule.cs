using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace StaffManagement.Model
{
    class ScheduleView
    {
        public int scheduleId { get; set; }
        public string name { get; set; }
        public Nullable<System.DateTime> workDate { get; set; }
        public string startWorkHour { get; set; }
        public string endWorkHour { get; set; }
    }
    class Odata_ScheduleView
    {
        [JsonProperty("odata.metadata")]
        public string Metadata { get; set; }
        [JsonProperty("value")]
        public List<ScheduleView> ScheduleViewList { get; set; }
    }
}
