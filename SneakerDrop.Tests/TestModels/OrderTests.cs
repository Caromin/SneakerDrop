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
            OrderId = 1,
            OrderGroupNumber = 22,
            Quantity = 2,
            ShippingStatus = "pending",
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

        [Fact]
        public void Test_AddOrderById()
        {
            var sut = OrderHelper.AddOrderById(order);

            Assert.True(sut);
        }

        [Fact]
        public void Test_GetOrdersById()
        {
            var sut = OrderHelper.GetOrdersById(order);

            Assert.NotNull(sut);
        }

        [Fact(Skip = "nothing to delete")]
        public void Test_CancelOrderById()
        {
            var sut = OrderHelper.CancelOrderByOrderId(order);

            Assert.True(sut);
        }
        [Fact]
        public void Test_AddOrders()
        {
            var sut = new OrderAndPaymentViewModel
            {
                HelperType = "add",
                Timestamp = DateTime.UtcNow,
                OrderGroupNumber = 23,
                Quantity = 1,
                ShippingStatus = "pending",
                PaymentId = 2,
                ListingId = 12,
                UserId = 1,

            };
            var test = sut.AddOrCancelOrders(sut);
            Assert.True(test);
        }
    }
}
