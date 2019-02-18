using System;
using SneakerDrop.Code.Helpers;
using SneakerDrop.Domain.Models;
using SneakerDrop.Mvc.Models;
using Xunit;

namespace SneakerDrop.Tests.TestModels
{
    public class OrderTests
    {
        public static Orders order = new Orders
        {
            //OrderGroupNumber = 22,
            Quantity = 2,
            //Timestamp = DateTime.Now,
            ShippingStatus = "pending",
            Listing = new Listing
            {
                ListingId = 19,
                UserSetPrice = (decimal)220.00,
                Quantity = 3,
                Size = "10",
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
    },
            Payment = new Payment
            {
                PaymentId = 1,
                CCNumber = 1111222233334444,
                CCUserName = "Christian Aromin",
                Month = 01,
                Year = 22,
                CVV = 123,
                User = new User
                {
                    UserId = 1,
                    Username = "ian2519",
                    Password = "Password",
                    Firstname = "Ian",
                    Lastname = "Nai",
                    Email = "Email@email.com"
                }
            },
            User = new User
            {
                UserId = 1,
                Username = "ian2519",
                Password = "Password",
                Firstname = "Ian",
                Lastname = "Nai",
                Email = "Email@email.com"
            }
        };

        //[Fact(Skip ="timestamp dosne't match datetime")]
        //public void Test_AddOrderById()
        //{
        //    var sut = OrderHelper.AddOrderById(order);

        //    Assert.True(sut);
        //}

        [Fact]
        public void Test_GetOrdersById()
        {
            var sut = OrderHelper.GetOrdersById(order);

            Assert.NotNull(sut);
        }
        [Fact]
        public void Test_GetAllOrdersById()
        {
            User user = new User
            {
                UserId = 1,
            };
            var sut = OrderHelper.GetAllOrdersById(user.UserId);

            Assert.NotEmpty(sut);
        }

        [Fact(Skip = "nothing to delete")]
        public void Test_CancelOrderById()
        {
            var sut = OrderHelper.CancelOrderByOrderId(order);

            Assert.True(sut);
        }
        //[Fact]
        //public void Test_AddOrders()
        //{
        //    var sut = new OrderAndPaymentViewModel
        //    {
        //        HelperType = "add",
        //        Timestamp = DateTime.UtcNow,
        //        OrderGroupNumber = 23,
        //        Quantity = 1,
        //        ShippingStatus = "pending",
        //        PaymentId = 2,
        //        ListingId = 12,
        //        UserId = 1,
        //    };
        //    var test = sut.AddOrCancelOrders(sut);
        //    Assert.True(test);
        //}
    }
}
