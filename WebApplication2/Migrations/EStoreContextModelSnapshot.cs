﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication2.Data;

#nullable disable

namespace WebApplication2.Migrations
{
    [DbContext(typeof(EStoreContext))]
    partial class EStoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebApplication2.Models.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Brands", (string)null);
                });

            modelBuilder.Entity("WebApplication2.Models.Laptop", b =>
                {
                    b.Property<Guid>("LaptopsNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<int>("Condition")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("LaptopsNumber");

                    b.HasIndex("BrandId");

                    b.ToTable("Laptops", (string)null);
                });

            modelBuilder.Entity("WebApplication2.Models.LaptopStore", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StoreNumber")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LaptopsNumber")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id", "StoreNumber", "LaptopsNumber");

                    b.HasIndex("LaptopsNumber");

                    b.HasIndex("StoreNumber");

                    b.ToTable("LaptopStore", (string)null);
                });

            modelBuilder.Entity("WebApplication2.Models.Store", b =>
                {
                    b.Property<Guid>("StoreNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Province")
                        .HasColumnType("int");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StoreNumber");

                    b.ToTable("Store", (string)null);
                });

            modelBuilder.Entity("WebApplication2.Models.Laptop", b =>
                {
                    b.HasOne("WebApplication2.Models.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("WebApplication2.Models.LaptopStore", b =>
                {
                    b.HasOne("WebApplication2.Models.Laptop", "Laptops")
                        .WithMany("LaptopStores")
                        .HasForeignKey("LaptopsNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication2.Models.Store", "Store")
                        .WithMany("LaptopStores")
                        .HasForeignKey("StoreNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Laptops");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("WebApplication2.Models.Laptop", b =>
                {
                    b.Navigation("LaptopStores");
                });

            modelBuilder.Entity("WebApplication2.Models.Store", b =>
                {
                    b.Navigation("LaptopStores");
                });
#pragma warning restore 612, 618
        }
    }
}
