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
    
    public partial class Journal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Journal()
        {
            this.Images = new HashSet<Images>();
        }
    
        public System.Guid ID { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public Nullable<System.Guid> PlantID { get; set; }
        public string UserID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Images> Images { get; set; }
        public virtual Plant Plant { get; set; }
    }
}
