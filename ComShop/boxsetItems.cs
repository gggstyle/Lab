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
    
    public partial class boxsetItems
    {
        public int biid { get; set; }
        public int bid { get; set; }
        public Nullable<int> pid { get; set; }
    
        public virtual boxsets boxsets { get; set; }
        public virtual products products { get; set; }
    }
}
