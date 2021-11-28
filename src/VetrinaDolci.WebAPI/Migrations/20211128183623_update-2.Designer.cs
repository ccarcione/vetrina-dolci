﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VetrinaDolci.WebAPI;

namespace VetrinaDolci.WebAPI.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20211128183623_update-2")]
    partial class update2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("VetrinaDolci.WebAPI.Models.Dolce", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DolceInVenditaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("IngPrincipale")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT");

                    b.Property<int>("Persone")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Preparazione")
                        .HasColumnType("TEXT");

                    b.Property<string>("Prezzo")
                        .HasColumnType("TEXT");

                    b.Property<string>("TipoPiatto")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Dolci");
                });

            modelBuilder.Entity("VetrinaDolci.WebAPI.Models.DolceInVendita", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Disponibilita")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DolceId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("InVenditaDa")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DolceId");

                    b.ToTable("DolciInVendita");
                });

            modelBuilder.Entity("VetrinaDolci.WebAPI.Models.Ingrediente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Colesterolo")
                        .HasColumnType("REAL");

                    b.Property<double>("Fibra")
                        .HasColumnType("REAL");

                    b.Property<double>("Grassi")
                        .HasColumnType("REAL");

                    b.Property<double>("Kcal")
                        .HasColumnType("REAL");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<double>("Proteine")
                        .HasColumnType("REAL");

                    b.Property<double>("Zuccheri")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Ingredienti");
                });

            modelBuilder.Entity("VetrinaDolci.WebAPI.Models.IngredientiDolce", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DolceId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IngredienteId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Quantita")
                        .HasColumnType("TEXT");

                    b.Property<string>("UnitaDiMisura")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DolceId");

                    b.HasIndex("IngredienteId");

                    b.ToTable("IngredientiDolce");
                });

            modelBuilder.Entity("VetrinaDolci.WebAPI.Models.DolceInVendita", b =>
                {
                    b.HasOne("VetrinaDolci.WebAPI.Models.Dolce", "Dolce")
                        .WithMany("DolciInVendita")
                        .HasForeignKey("DolceId");

                    b.Navigation("Dolce");
                });

            modelBuilder.Entity("VetrinaDolci.WebAPI.Models.IngredientiDolce", b =>
                {
                    b.HasOne("VetrinaDolci.WebAPI.Models.Dolce", "Dolce")
                        .WithMany("IngredientiDolce")
                        .HasForeignKey("DolceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VetrinaDolci.WebAPI.Models.Ingrediente", "Ingrediente")
                        .WithMany("IngredientiDolce")
                        .HasForeignKey("IngredienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dolce");

                    b.Navigation("Ingrediente");
                });

            modelBuilder.Entity("VetrinaDolci.WebAPI.Models.Dolce", b =>
                {
                    b.Navigation("DolciInVendita");

                    b.Navigation("IngredientiDolce");
                });

            modelBuilder.Entity("VetrinaDolci.WebAPI.Models.Ingrediente", b =>
                {
                    b.Navigation("IngredientiDolce");
                });
#pragma warning restore 612, 618
        }
    }
}
