//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ComplexApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Complex
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Complex()
        {
            this.Buildings = new HashSet<Building>();
        }
    
        public short complexId { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public short noBuildings { get; set; }
        public string contactNumber { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Building> Buildings { get; set; }
    }
}
