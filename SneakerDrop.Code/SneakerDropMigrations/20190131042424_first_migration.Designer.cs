﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SneakerDrop.Code;

namespace SneakerDrop.Code.SneakerDropMigrations
{
    [DbContext(typeof(SneakerDropDbContext))]
    [Migration("20190131042424_first_migration")]
    partial class first_migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SneakerDrop.Domain.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("User");

                    b.HasKey("AddressId");

                    b.HasIndex("User");

                    b.ToTable("Address","User");
                });

            modelBuilder.Entity("SneakerDrop.Domain.Models.Listing", b =>
                {
                    b.Property<int>("ListingId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProductInfo");

                    b.Property<int>("Quantity");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("User");

                    b.Property<decimal>("UserSetPrice");

                    b.HasKey("ListingId");

                    b.HasIndex("ProductInfo");

                    b.HasIndex("User");

                    b.ToTable("Listing","Store");
                });

            modelBuilder.Entity("SneakerDrop.Domain.Models.Orders", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Listing");

                    b.Property<int>("OrderGroupNumber");

                    b.Property<int>("Payment");

                    b.Property<int>("Quantity");

                    b.Property<string>("ShippingStatus")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("User");

                    b.HasKey("OrderId");

                    b.HasIndex("Listing");

                    b.HasIndex("Payment");

                    b.HasIndex("User");

                    b.ToTable("Orders","User");
                });

            modelBuilder.Entity("SneakerDrop.Domain.Models.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CCNumber");

                    b.Property<string>("CCUserName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("CVV");

                    b.Property<int>("Month");

                    b.Property<int>("User");

                    b.Property<int>("Year");

                    b.HasKey("PaymentId");

                    b.HasIndex("User");

                    b.ToTable("Payment","User");
                });

            modelBuilder.Entity("SneakerDrop.Domain.Models.ProductInfo", b =>
                {
                    b.Property<int>("ProductInfoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<int>("DisplayPrice");

                    b.Property<string>("ProductTitle")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("ReleaseDate")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ProductInfoId");

                    b.ToTable("Product","Store");
                });

            modelBuilder.Entity("SneakerDrop.Domain.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("UserId");

                    b.ToTable("User","User");
                });

            modelBuilder.Entity("SneakerDrop.Domain.Models.Address", b =>
                {
                    b.HasOne("SneakerDrop.Domain.Models.User", "UserId")
                        .WithMany()
                        .HasForeignKey("User")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SneakerDrop.Domain.Models.Listing", b =>
                {
                    b.HasOne("SneakerDrop.Domain.Models.ProductInfo", "ProductId")
                        .WithMany()
                        .HasForeignKey("ProductInfo")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SneakerDrop.Domain.Models.User", "UserId")
                        .WithMany()
                        .HasForeignKey("User")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SneakerDrop.Domain.Models.Orders", b =>
                {
                    b.HasOne("SneakerDrop.Domain.Models.Listing", "ListingId")
                        .WithMany()
                        .HasForeignKey("Listing")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SneakerDrop.Domain.Models.Payment", "PaymentId")
                        .WithMany()
                        .HasForeignKey("Payment")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SneakerDrop.Domain.Models.User", "UserId")
                        .WithMany()
                        .HasForeignKey("User")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SneakerDrop.Domain.Models.Payment", b =>
                {
                    b.HasOne("SneakerDrop.Domain.Models.User", "UserId")
                        .WithMany()
                        .HasForeignKey("User")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
