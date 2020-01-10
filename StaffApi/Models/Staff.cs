//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StaffApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Staff
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Staff()
        {
            this.LeaveRequests = new HashSet<LeaveRequest>();
            this.Schedules = new HashSet<Schedule>();
            this.StaffVacations = new HashSet<StaffVacation>();
        }
    
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
    
        public virtual Department Department { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LeaveRequest> LeaveRequests { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Schedule> Schedules { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StaffVacation> StaffVacations { get; set; }
    }
}
