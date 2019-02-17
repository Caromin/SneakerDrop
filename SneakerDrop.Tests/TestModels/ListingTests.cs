using SneakerDrop.Code.Helpers;
using SneakerDrop.Domain.Models;
using SneakerDrop.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SneakerDrop.Tests.TestModels
{
    public class ListingTests
    {
        public static CreateNewListingViewModel listing = new CreateNewListingViewModel
        {
            UserSetPrice = (decimal)220.00,
            Quantity = 3,
            Size = "10",
            UserId = 7,
            ProductInfoId = 8, 
        };

        public static Listing listing2 = new Listing
        {
            ListingId = 11,
            ProductInfoId = 1,
            UserSetPrice = (decimal)100.00,
            Quantity = 1,
            Size = "8",
            User = new User
            {
                UserId = 1
            }
        };
        [Fact]
        public void Test_AddListing()
        {
            var sut = listing.AddListingToDb(listing);

            Assert.True(sut);
        }
        [Fact]
        public void Test_GetAllListingByProductInfoId()
        {
            var sut = ListingHelper.GetAllListingsByProductInfoId(listing2.ProductInfoId);

            Assert.NotEmpty(sut);
        }
        [Fact]
        public void Test_GetAllListingByListingId()
        {
            var sut = ListingHelper.GetallListingsByListingId(listing2.ListingId);

            Assert.NotEmpty(sut);
        }
        [Fact]
        public void Test_GetAllListingById()
        {
            var sut = ListingHelper.GetAllListingById(listing.UserId);

            Assert.NotEmpty(sut);
        }
        [Fact]
        public void Test_GetProductIdByListingId()
        {
            var sut = ListingHelper.GetProductIdByListingId(listing2.ListingId);

            Assert.NotNull(sut);
        }
    }
}
