﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CONGTY> CONGTies { get; set; }
        public virtual DbSet<DONVI> DONVIs { get; set; }
        public virtual DbSet<DVT> DVTs { get; set; }
        public virtual DbSet<HANGHOA> HANGHOAs { get; set; }
        public virtual DbSet<NHACUNGCAP> NHACUNGCAPs { get; set; }
        public virtual DbSet<SYS_FUNC> SYS_FUNC { get; set; }
        public virtual DbSet<SYS_GROUP> SYS_GROUP { get; set; }
        public virtual DbSet<SYS_REPORT> SYS_REPORT { get; set; }
        public virtual DbSet<SYS_RIGHT> SYS_RIGHT { get; set; }
        public virtual DbSet<SYS_RIGHT_REP> SYS_RIGHT_REP { get; set; }
        public virtual DbSet<SYS_USER> SYS_USER { get; set; }
        public virtual DbSet<TONKHO> TONKHOes { get; set; }
        public virtual DbSet<XUATXU> XUATXUs { get; set; }
        public virtual DbSet<CHUNGTU> CHUNGTUs { get; set; }
        public virtual DbSet<CHUNGTU_CT> CHUNGTU_CT { get; set; }
    }
}
