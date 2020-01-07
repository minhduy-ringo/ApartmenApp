using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace StaffManagement.Model
{
    class TaskType
    {
        public short taskTypeId { get; set; }
        public string taskTypeName { get; set; }
    }
    class Odata_TaskType
    {
        [JsonProperty("odata.metadata")]
        public string Metadata { get; set; }
        [JsonProperty("value")]
        public List<TaskType> TaskTypeList { get; set; }
    }
}
