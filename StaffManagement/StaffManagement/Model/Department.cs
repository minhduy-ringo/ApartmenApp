using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace StaffManagement.Model
{
    public class Department
    {
        public short departmentId { get; set; }
        public string name { get; set; }
        public Nullable<int> managerId { get; set; }
    }

    public class OData_Department
    {
        [JsonProperty("odata.metadata")]
        public string Metadata { get; set; }
        [JsonProperty("value")]
        public List<Department> DepartmentsList { get; set; }
    }
}
