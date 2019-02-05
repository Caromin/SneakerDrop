using SneakerDrop.Code.Helpers;
using SneakerDrop.Domain.Models;
using System;
using Xunit;

namespace SneakerDrop.Tests.TestModels
{
    public class AddressTests
    {
        public User testuser = new User
        {
            UserId = 1,
            Username = "ian2519",
            Password = "Password",
            Firstname = "Ian",
            Lastname = "Nai",
            Email = "Email@email.com"
        };

        public Address address = new Address
        {
            Street = "4508 Burnhill Dr",
            City = "Plano",
            State = "TX",
            PostalCode = "75098",

        };
        //[Fact]
        //public void Test_AddAddressByUser()
        //{
        //    var sut = AddressHelper.AddAddress(address);

        //    Assert.True(sut);

        //}
        [Fact]
        public void Test_GetAddressInfoById()
        {
            var test = new AddressTests();
            var value = test.address.AddressId;
            var sut = AddressHelper.GetAddressInfoById(address);

            Assert.Equal(value, sut.AddressId);
        }
    }
}
