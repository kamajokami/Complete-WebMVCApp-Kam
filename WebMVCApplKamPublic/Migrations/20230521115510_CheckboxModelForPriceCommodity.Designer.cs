﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebMVCApplKamPublic.Data;

#nullable disable

namespace WebMVCApplKamPublic.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230521115510_CheckboxModelForPriceCommodity")]
    partial class CheckboxModelForPriceCommodity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.4");

            modelBuilder.Entity("WebMVCApplKamPublic.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("WebMVCApplKamPublic.Models.ViewModels.PriceViewModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Currency")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsBuyerMan")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsBuyerWoman")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsCommodityActive")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsPropertyOwner")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsSale")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PriceMoney")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("SendConsent")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Width")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("ComodityPrices");
                });
#pragma warning restore 612, 618
        }
    }
}
