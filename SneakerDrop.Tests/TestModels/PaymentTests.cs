using System;
using SneakerDrop.Code.Helpers;
using SneakerDrop.Domain.Models;
using SneakerDrop.Mvc.Models;
using Xunit;

namespace SneakerDrop.Tests.TestModels
{
    public class PaymentTests
    {
        public static Payment payment = new Payment()
        {
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
        [Fact]
        public void Test_GetPaymentById()
        {
            var sut = PaymentHelper.GetPaymentById(payment);

            Assert.Equal(2, sut.Count);
        }

        [Fact(Skip = "working, avoid deleting from db")]
        public void Test_DeletePaymentById()
        {
            var sut = PaymentHelper.DeletePaymentByPaymentId(payment);

            Assert.True(sut);
        }
        [Fact(Skip ="working")]
        public void Test_AddPayment()
        {
            var sut = new PaymentViewModel
            {
                CCNumber = 4444333322221111,
                CCUserName = "Christian Aromin",
                Month = 01,
                Year = 22,
                CVV = 123,
                UserId = 1,
                HelperType = "add",
            };
            var test = sut.AddOrDeletePayments(sut);
            Assert.True(test);
        }
        [Fact(Skip ="Is working")]
        public void Test_DeletePayment()
        {
            var sut = new PaymentViewModel
            {
                PaymentId = 13
            };
            var test = sut.AddOrDeletePayments(sut);

            Assert.True(test);
        }
        [Fact(Skip ="working but interfering with another test")]
        public void Test_ValidateNewPayment()
        {
            var sut = new PaymentViewModel
            {
                CCNumber = 1111222233334444
            };
            var test = sut.AddOrDeletePayments(sut);

            Assert.True(test);

        }
        [Fact]
        public void Test_GetAllPayment()
        {
            var sut = new PaymentViewModel
            {
                UserId = 1
            };
            var test = sut.GetAllPayments(sut);

            Assert.NotEmpty(test);
        }
    }

}
