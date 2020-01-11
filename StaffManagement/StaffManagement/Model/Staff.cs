using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace StaffManagement.Model
{
    public class Staff
    {
        public int staffId { get; set; }
        public short departmentId { get; set; }
        public short complexId { get; set; }
        public string name { get; set; }
        public System.DateTime birthday { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string mobile { get; set; }
        public System.DateTime joinDate { get; set; }
        public Nullable<System.DateTime> leaveDate { get; set; }
        public bool isManager { get; set; }
    }

    public class OData_Staff
    {
        [JsonProperty("odata.metadata")]
        public string Metadata { get; set; }
        [JsonProperty("value")]
        public List<Staff> StaffList { get;set; }
    }
}
