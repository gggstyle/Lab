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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class apd64_62011212131Entities8 : DbContext
    {
        public apd64_62011212131Entities8()
            : base("name=apd64_62011212131Entities8")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<boxsetItems> boxsetItems { get; set; }
        public virtual DbSet<boxsets> boxsets { get; set; }
        public virtual DbSet<customers> customers { get; set; }
        public virtual DbSet<employee> employee { get; set; }
        public virtual DbSet<orderItems> orderItems { get; set; }
        public virtual DbSet<orders> orders { get; set; }
        public virtual DbSet<products> products { get; set; }
    }
}
