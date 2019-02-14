using SneakerDrop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SneakerDrop.Tests.TestModels
{
    public class ListingTests
    {
        public static Listing listing = new Listing
        {
            UserSetPrice = (decimal)220.00,
            Quantity = 3,
            Size = "9.5",
            User = new User
            {
                UserId = 7,
                Firstname = "Henok",
                Lastname = "Tesfaye",
                Username = "OaksTree",
                Password = "1234",
                Email = "henoktothemax@gmail.com"
            },
            ProductInfo = new ProductInfo
            {
                ProductInfoId = 8,
                Brand = new Brand
                {
                    BrandId = 2,
                    BrandName = "Adidas"
                },
                Type = new Domain.Models.Type
                {
                    TypeId = 3,
                    TypeName = "Basketball Shoes"
                },
                ProductTitle = "Adidas Yeezy Boost 350 V2 Static",
                Description = "This Yeezy 350 V2 comes with a grey and white upper and a white sole.",
                DisplayPrice = 220,
                ReleaseDate = "12/27/2018",
                Color = "STATIC/STATIC/STATIC"
            },
        };
    
        [Fact]
        public void Test_AddListing()
        {
            var sut = listing.AddListing(listing);

            Assert.True(sut);
        }
    }
}
