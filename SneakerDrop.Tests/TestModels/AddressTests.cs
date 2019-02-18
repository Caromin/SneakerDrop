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
            AddressId = 4,
            Street = "1234 fake street",
            City = "Plano",
            State = "TX",
            PostalCode = "75000",
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
        [Fact]
        public void Test_GetAllAddresses()
        {

            var sut = new AddressViewModel
            {
                UserId = 1
            };
            var test = sut.GetAllAddresses(sut);


            Assert.NotEmpty(test);
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
        [Fact]
        public void Test_GetAddressInfoByAddressId()
        {
            var sut = AddressHelper.GetAddressInfoByAddressId(address.User.UserId);

            Assert.NotNull(sut);
        }
    }
}



