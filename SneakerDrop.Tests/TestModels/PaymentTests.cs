using System;
using SneakerDrop.Code.Helpers;
using SneakerDrop.Domain.Models;
using Xunit;

namespace SneakerDrop.Tests.TestModels
{
    public class PaymentTests
    {
        public static Payment payment = new Payment()
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

        };

        [Fact(Skip = "paused for now")]
        public void Test_AddUser()
        {
            var sut = PaymentHelper.AddPaymentById(payment);

            Assert.True(sut);
        }

        [Fact]
        public void Test_GetPaymentById()
        {
            var sut = PaymentHelper.GetPaymentById(payment);

            Assert.Equal(1, sut.Count);
        }

        [Fact]
        public void Test_EditPaymentById()
        {
            var sut = PaymentHelper.EditPaymentById(payment);

            Assert.True(sut);
        }

        [Fact]
        public void Test_DeletePaymentById()
        {
            var sut = PaymentHelper.DeletePaymentById(payment);

            Assert.True(sut);
        }
    }

}
