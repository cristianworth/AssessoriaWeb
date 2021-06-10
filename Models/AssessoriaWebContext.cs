using System;
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
        public System.Data.Entity.DbSet<AssessoriaWeb.Models.Avaliacao> Avaliacaos { get; set; }
        public System.Data.Entity.DbSet<AssessoriaWeb.Models.Endereco> Enderecoes { get; set; }
        public System.Data.Entity.DbSet<AssessoriaWeb.Models.Assessor> Assessors { get; set; }
        public System.Data.Entity.DbSet<AssessoriaWeb.Models.Treinamento> Treinamentoes { get; set; }
        public System.Data.Entity.DbSet<AssessoriaWeb.Models.Atividade> Atividades { get; set; }
        public System.Data.Entity.DbSet<AssessoriaWeb.Models.Turma> Turmas { get; set; }
        public System.Data.Entity.DbSet<AssessoriaWeb.Models.Nutricionista> Nutricionistas { get; set; }
        public System.Data.Entity.DbSet<AssessoriaWeb.Models.PlanoAlimentar> PlanoAlimentars { get; set; }
        public System.Data.Entity.DbSet<AssessoriaWeb.Models.AtletaTurma> AtletaTurmas { get; set; }
        public System.Data.Entity.DbSet<AssessoriaWeb.Models.AtividadeTreinamento> AtividadeTreinamentos { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoa>().HasMany(e => e.Atletas).WithRequired(e => e.Pessoa).WillCascadeOnDelete(true);
            modelBuilder.Entity<Pessoa>().HasMany(e => e.Assessores).WithRequired(e => e.Pessoa).WillCascadeOnDelete(true);
            modelBuilder.Entity<Pessoa>().HasMany(e => e.Enderecos).WithRequired(e => e.Pessoa).WillCascadeOnDelete(true);

            modelBuilder.Entity<Atleta>().HasMany(e => e.Avaliacoes).WithRequired(e => e.Atleta).WillCascadeOnDelete(true);
            modelBuilder.Entity<Atleta>().HasMany(e => e.Treinamentos).WithRequired(e => e.Atleta).WillCascadeOnDelete(true);
            modelBuilder.Entity<Atleta>().HasMany(e => e.PlanosAlimentares).WithRequired(e => e.Atleta).WillCascadeOnDelete(true);

            modelBuilder.Entity<Assessor>().HasMany(e => e.Treinamentos).WithRequired(e => e.Assessor).WillCascadeOnDelete(true);
            modelBuilder.Entity<Assessor>().HasMany(e => e.Turmas).WithRequired(e => e.Assessor).WillCascadeOnDelete(true);

            modelBuilder.Entity<Nutricionista>().HasMany(e => e.PlanosAlimentares).WithRequired(e => e.Nutricionista).WillCascadeOnDelete(true);

            /*N pra N*/
            modelBuilder.Entity<Treinamento>().HasMany(e => e.Atividades).WithMany(e => e.Treinamentos);
            
            /*Inicio da relação N pra N de AtletaTurma*/
            modelBuilder.Entity<AtletaTurma>()
                .HasKey(c => new { c.atl_id, c.trm_id });

            modelBuilder.Entity<Atleta>()
                .HasMany(c => c.AtletaTurmas)
                .WithRequired()
                .HasForeignKey(c => c.atl_id);

            modelBuilder.Entity<Turma>()
                .HasMany(c => c.AtletaTurmas)
                .WithRequired()
                .HasForeignKey(c => c.trm_id);
            /*Fim da relação N pra N de AtletaTurma*/

        }

    }
}
