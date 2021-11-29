using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetrinaDolci.WebAPI.Models;

namespace VetrinaDolci.WebAPI
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Dolce> Dolci { get; set; }
        public DbSet<DolceInVendita> DolciInVendita { get; set; }
        public DbSet<Ingrediente> Ingredienti { get; set; }
        public DbSet<IngredientiDolce> IngredientiDolce { get; set; }

        public string DbPath { get; private set; }

        public ApplicationContext()
        {
            var folder = Environment.CurrentDirectory;
            DbPath = $"{folder}{System.IO.Path.DirectorySeparatorChar}vetrina-dolci.db";
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        // Creating and configuring a model
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Dolce-Ingrediente mxm
            modelBuilder.Entity<IngredientiDolce>()
                .HasOne(x => x.Dolce)
                .WithMany(x => x.IngredientiDolce)
                .HasForeignKey(x => x.DolceId);
            modelBuilder.Entity<IngredientiDolce>()
                .HasOne(x => x.Ingrediente)
                .WithMany(x => x.IngredientiDolce)
                .HasForeignKey(x => x.IngredienteId);

            // seed data
            //modelBuilder.Entity<Dolce>()
            //    .HasData(data);
        }
    }
}
