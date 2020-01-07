using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace StaffManagement.Model
{
    public class Staff
    {
        public int staffId { get; set; }
        public string name { get; set; }
        public Nullable<System.DateTime> birthday { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string mobile { get; set; }
        public Nullable<short> departmentId { get; set; }
        public Nullable<short> complexId { get; set; }
        public Nullable<System.DateTime> joinDate { get; set; }
        public Nullable<System.DateTime> leaveDate { get; set; }
    }

    public class OData_Staff
    {
        [JsonProperty("odata.metadata")]
        public string Metadata { get; set; }
        [JsonProperty("value")]
        public List<Staff> StaffList { get;set; }
    }
}
