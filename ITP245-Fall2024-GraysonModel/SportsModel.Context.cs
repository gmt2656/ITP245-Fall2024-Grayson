﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ITP245_Fall2024_GraysonModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class SportsEntities : DbContext
    {
        public SportsEntities()
            : base("name=SportsEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<SystemOption> SystemOptions { get; set; }
        public virtual DbSet<Division> Divisions { get; set; }
        public virtual DbSet<Field> Fields { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Office> Offices { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Roster> Rosters { get; set; }
        public virtual DbSet<SportsImage> SportsImages { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
    }
}
