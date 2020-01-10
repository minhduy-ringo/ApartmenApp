using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace StaffManagement.Model
{
    class TaskStaff
    {
        public int taskId { get; set; }
        public int staffId { get; set; }
    }
    class Odata_TaskStaff
    {
        [JsonProperty("odata.metadata")]
        public string Metadata { get; set; }
        [JsonProperty("value")]
        public List<TaskStaff> TaskStaffList { get; set; }
    }
}
