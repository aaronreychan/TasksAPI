﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.Models
{
    using Base;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class CBCTestEntities : BaseDbContext
    {
        public CBCTestEntities()
            : base("name=CBCTestEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Lookup> Lookups { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
    }
}
