﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartShopping.Data;

#nullable disable

namespace SmartShopping.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20221107200138_InitialTableCreate")]
    partial class InitialTableCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc.1.22426.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProductProductTag", b =>
                {
                    b.Property<Guid>("ProductsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TagsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProductsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("ProductProductTag");
                });

            modelBuilder.Entity("ProductShop", b =>
                {
                    b.Property<Guid>("ProductsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ShopsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProductsId", "ShopsId");

                    b.HasIndex("ShopsId");

                    b.ToTable("ProductShop");
                });

            modelBuilder.Entity("SmartShopping.Models.PriceRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CheckDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ShopId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("ProductId");

                    b.HasIndex("ShopId");

                    b.ToTable("PriceRecords");
                });

            modelBuilder.Entity("SmartShopping.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SmartShopping.Models.ProductTag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("ProductTags");
                });

            modelBuilder.Entity("SmartShopping.Models.Shop", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("SmartShopping.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProductProductTag", b =>
                {
                    b.HasOne("SmartShopping.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartShopping.Models.ProductTag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProductShop", b =>
                {
                    b.HasOne("SmartShopping.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartShopping.Models.Shop", null)
                        .WithMany()
                        .HasForeignKey("ShopsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SmartShopping.Models.PriceRecord", b =>
                {
                    b.HasOne("SmartShopping.Models.Product", "Product")
                        .WithMany("PriceRecords")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartShopping.Models.Shop", "Shop")
                        .WithMany()
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("SmartShopping.Models.Product", b =>
                {
                    b.Navigation("PriceRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
