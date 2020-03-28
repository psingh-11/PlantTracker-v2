//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PlantDAL.EDMX
{
    using System;
    using System.Collections.Generic;
    
    public partial class Plant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Plant()
        {
            this.Images = new HashSet<Images>();
            this.Journal = new HashSet<Journal>();
            this.Plant11 = new HashSet<Plant>();
            this.Plant12 = new HashSet<Plant>();
        }
    
        public System.Guid ID { get; set; }
        public string UserID { get; set; }
        public System.Guid ParentOneID { get; set; }
        public Nullable<System.Guid> ParentTwoID { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public string Type { get; set; }
        public string Genus { get; set; }
        public string Species { get; set; }
        public string SubSpecies { get; set; }
        public Nullable<System.Guid> CustomValueOneID { get; set; }
        public Nullable<System.Guid> CustomValueTwoD { get; set; }
        public Nullable<System.Guid> CustomValueThreeID { get; set; }
        public Nullable<System.Guid> CustomValueFourID { get; set; }
        public Nullable<System.Guid> CustomValueFiveID { get; set; }
        public string Notes { get; set; }
    
        public virtual CustomValues1 CustomValues1 { get; set; }
        public virtual CustomValues2 CustomValues2 { get; set; }
        public virtual CustomValues3 CustomValues3 { get; set; }
        public virtual CustomValues4 CustomValues4 { get; set; }
        public virtual CustomValues5 CustomValues5 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Images> Images { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Journal> Journal { get; set; }
        public virtual Plant Plant1 { get; set; }
        public virtual Plant Plant2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Plant> Plant11 { get; set; }
        public virtual Plant Plant3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Plant> Plant12 { get; set; }
        public virtual Plant Plant4 { get; set; }
    }
}