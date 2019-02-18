using SneakerDrop.Code.Helpers;
using SneakerDrop.Domain.Models;
using SneakerDrop.Mvc.Models;
using System;
using Xunit;

namespace SneakerDrop.Tests.TestModels
{
    public class AddressTests
    {
        public static Address address = new Address()
        {
            AddressId = 2,
            Street = "4508 Burnhill Dr",
            City = "Frisco",
            State = "TX",
            PostalCode = "75024",
            User = new User
            {
                UserId = 2,
                Username = "ian2519",
                Password = "Password",
                Firstname = "Ian",
                Lastname = "Nai",
                Email = "Email@email.com"
            }
        };
        [Fact(Skip = "identity insert")]
        public void Test_AddAddresesById()
        {
            var sut = AddressHelper.AddAddressById(address);

            Assert.NotNull(sut);

        }
        [Fact]
        public void Test_GetAddressInfoById()
        {

            var sut = AddressHelper.GetAddressInfoById(address);

            Assert.NotNull(sut);
        }

        [Fact(Skip = "working")]
        public void Test_EditAddressInfoById()
        {
            var sut = AddressHelper.EditAddressInfoById(address);

            Assert.True(sut);
        }

        [Fact(Skip = "working")]
        public void Test_DeleteAddressInfoById()
        {
            var sut = AddressHelper.DeleteAddressInfoById(address);

            Assert.True(sut);
        }

        [Fact(Skip = "edit works")]
        public void Test_EditAddresses()
        {
            var sut = new AddressViewModel
            {
                AddressId = 1,
                Street = "4508 Burnhill Dr",
                City = "Plano",
                State = "TX",
                PostalCode = "75098",
                HelperType = "edit",
                UserId = 1

            };
            var test = sut.AddEditDeleteAddresses(sut);

            Assert.True(test);
        }
        [Fact(Skip = "working")]
        public void Test_AddAddress()
        {
            var sut = new AddressViewModel
            {
                Street = "4508 Burnhill Dr",
                City = "Plano",
                State = "TX",
                PostalCode = "75098",
                HelperType = "add",
                UserId = 1
            };
            var test = sut.AddEditDeleteAddresses(sut);

            Assert.True(test);
        }
    }
}



