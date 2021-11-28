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
    [Migration("20211128014912_init")]
    partial class init
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

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<string>("Prezzo")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DolceInVenditaId");

                    b.ToTable("Dolci");
                });

            modelBuilder.Entity("VetrinaDolci.WebAPI.Models.DolceInVendita", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Disponibilita")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("InVenditaDa")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("DolciInVendita");
                });

            modelBuilder.Entity("VetrinaDolci.WebAPI.Models.Ingrediente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Ingredienti");
                });

            modelBuilder.Entity("VetrinaDolci.WebAPI.Models.IngredientiDolce", b =>
                {
                    b.Property<int>("DolceId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IngredienteId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantita")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UnitaDiMisura")
                        .HasColumnType("TEXT");

                    b.HasKey("DolceId", "IngredienteId");

                    b.HasIndex("IngredienteId");

                    b.ToTable("IngredientiDolce");
                });

            modelBuilder.Entity("VetrinaDolci.WebAPI.Models.Dolce", b =>
                {
                    b.HasOne("VetrinaDolci.WebAPI.Models.DolceInVendita", "DolceInVendita")
                        .WithMany()
                        .HasForeignKey("DolceInVenditaId");

                    b.Navigation("DolceInVendita");
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