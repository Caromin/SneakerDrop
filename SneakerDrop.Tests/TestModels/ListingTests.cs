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
        };
        [Fact]
        public void Test_AddListing()
        {
            var sut = listing.AddListingToDb(listing);

            Assert.True(sut);
        }
    }
}
