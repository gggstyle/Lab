//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ComShop
{
    using System;
    using System.Collections.Generic;
    
    public partial class boxsets
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public boxsets()
        {
            this.boxsetItems = new HashSet<boxsetItems>();
        }
    
        public int bid { get; set; }
        public Nullable<int> price { get; set; }
        public string detail { get; set; }
        public Nullable<int> status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<boxsetItems> boxsetItems { get; set; }
    }
}