﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VL_SL_Online_Form.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SLVLOnlineEntities : DbContext
    {
        public SLVLOnlineEntities()
            : base("name=SLVLOnlineEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<EmailCredential> EmailCredential { get; set; }
        public virtual DbSet<Holiday> Holiday { get; set; }
        public virtual DbSet<LeaveForm> LeaveForm { get; set; }
        public virtual DbSet<OvertimeForm> OvertimeForm { get; set; }
        public virtual DbSet<UserAccount> UserAccount { get; set; }
        public virtual DbSet<TypeOfLeave> TypeOfLeave { get; set; }
    }
}
