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
            ListingId = 40,
        };
        [Fact]
        public void Test_AddListing()
        {
            var sut = listing.AddListingToDb(listing);

            Assert.True(sut);
        }
        [Fact]
        public void Test_GetListingInfoByIdForOrder()
        {
            Orders order = new Orders
            {
                OrderId = 43,
                Listing  = new Listing
                {
                    ListingId = 41,
                    ProductInfo = new ProductInfo
                    {
                        ProductInfoId = 17,
                    }
                }
            };
            var sut = ListingHelper.GetListingInfoByIdForOrder(order);

            Assert.NotNull(sut);
        }
        [Fact]
        public void Test_GetProductIdByListingId()
        {

            var sut = ListingHelper.GetProductIdByListingId(listing2.ListingId);

            Assert.NotNull(sut);
        }
        [Fact]
        public void Test_GetAllListingById()
        {
            User user = new User
            {
                UserId = 1,
            };
            var sut = ListingHelper.GetAllListingById(user.UserId);

            Assert.NotEmpty(sut);
        }
    }
}
