﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class apd64_62011212131Entities3 : DbContext
    {
        public apd64_62011212131Entities3()
            : base("name=apd64_62011212131Entities3")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<MyMenu> MyMenu { get; set; }
        public virtual DbSet<MyOrder> MyOrder { get; set; }
        public virtual DbSet<MyOrderItem> MyOrderItem { get; set; }
    }
}
