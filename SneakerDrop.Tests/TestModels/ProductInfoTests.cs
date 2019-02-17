using SneakerDrop.Code.Helpers;
using SneakerDrop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SneakerDrop.Tests.TestModels
{
    public class ProductInfoTests
    {
        public static ProductInfo productInfo = new ProductInfo
        {
            ProductInfoId = 1,
            Brand = new Brand
            {
                BrandId = 1,
                BrandName = "Nike"
            },
            Type = new Domain.Models.Type
            {
                TypeId = 3,
                TypeName = "Basketball Shoes"
            },
            ProductTitle = "Nike Blazer Mid 77 Habanero Red",
            Description = "This Blazer comes with a white upper, red Nike “Swoosh”, white midsole, and white sole.",
            DisplayPrice = 100,
            ReleaseDate = "1/19/2019",
            Color = "HABANERO RED/SAIL-WHITE"

        };
        [Fact]
        public void Test_FindPossibleMatches()
        {
            var sut = FindProductInfoHelper.FindPossibleMatches(productInfo);

            Assert.NotEmpty(sut);
        }
        [Fact]
        public void Test_SingleProductInfo()
        {
            var sut = FindProductInfoHelper.SingleProductInfo(productInfo);

            Assert.NotNull(sut);
        }
        [Fact]
        public void Test_SingleProductById()
        {
            var sut = FindProductInfoHelper.SingleProductById(productInfo.ProductInfoId);

            Assert.Equal(sut.ProductInfoId, productInfo.ProductInfoId);
        }
        [Fact]
        public void Test_GetAllRecentProducts()
        {
            var sut = FindProductInfoHelper.GetAllRecentProducts();

            Assert.NotEmpty(sut);
        } 
    }
}
