using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using dm = SneakerDrop.Domain.Models;


namespace SneakerDrop.Code
{
    public class SneakerDropDbContext : DbContext
    {
       public DbSet<dm.User> Users { get; set;}
       public DbSet<dm.Address>Addresses { get; set;}
       public DbSet<dm.Orders> Orders { get; set;}
        public DbSet <dm.Payment> Payment { get; set;}
        public DbSet<dm.Listing> Listings { get; set;}
        public DbSet<dm.ProductInfo> ProductInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("data source=oak2019.database.windows.net;initial catalog=SneakerDropDBv2;user id=sqladmin;password=Florida2019;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<dm.User>().HasKey(e => e.UserId);
            builder.Entity<dm.Address>().HasKey(e => e.AddressId);
            builder.Entity<dm.Orders>().HasKey(e => e.OrderId);
            builder.Entity<dm.Payment>().HasKey(e => e.PaymentId);
            builder.Entity<dm.Listing>().HasKey(e => e.ListingId);
            builder.Entity<dm.ProductInfo>().HasKey(e => e.ProductInfoId);
            
        }


        //modelbuilder.hasdefaultschema("User")
    }
}
