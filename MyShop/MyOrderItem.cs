//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyShop
{
    using System;
    using System.Collections.Generic;
    
    public partial class MyOrderItem
    {
        public int id { get; set; }
        public Nullable<int> oid { get; set; }
        public Nullable<int> mid { get; set; }
        public Nullable<int> count { get; set; }
        public Nullable<int> sum { get; set; }
    
        public virtual MyMenu MyMenu { get; set; }
        public virtual MyOrder MyOrder { get; set; }
    }
}
