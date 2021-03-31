﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MySql.Data.EntityFramework;

namespace AssessoriaWeb.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class AssessoriaWebContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
        public AssessoriaWebContext() : base("name=AssessoriaWebContext")
        {
        }

        public System.Data.Entity.DbSet<AssessoriaWeb.Models.Pessoa> Pessoas { get; set; }
        public System.Data.Entity.DbSet<AssessoriaWeb.Models.Atleta> Atletas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoa>().HasMany(e => e.Atletas).WithRequired(e => e.Pessoa).WillCascadeOnDelete(true);
            modelBuilder.Entity<Atleta>().HasMany(e => e.Avaliacoes).WithRequired(e => e.Atleta).WillCascadeOnDelete(true);
        }

        public System.Data.Entity.DbSet<AssessoriaWeb.Models.Avaliacao> Avaliacaos { get; set; }
    }
}
