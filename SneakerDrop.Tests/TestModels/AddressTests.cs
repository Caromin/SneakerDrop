using SneakerDrop.Code.Helpers;
using SneakerDrop.Domain.Models;
using System;
using Xunit;

namespace SneakerDrop.Tests.TestModels
{
    public class AddressTests
    {
        public Address sut2;
        public User user { get; set; }

        public AddressTests()
        {
            user = new User()
            {
                UserId = 2,
                Username = "ian2519",
                Password = "Password",
                Firstname = "Ian",
                Lastname = "Nai",
                Email = "Email@email.com"
            };
            sut2 = new Address()
            {
                AddressId = 4,
                Street = "4508 Burnhill Dr",
                City = "Plano",
                State = "TX",
                PostalCode = "75024",
                User = user
            };

            
        }
        [Fact]
        public void Test_AddAddressByUser()
        {
            var test = UserHelper.GetUserInfoById(user);
            var sut = AddressHelper.AddAddress(sut2);


            Assert.True(sut);

        }
        [Fact]
        public void Test_GetAddressInfoById()
        {
            var test = new AddressTests();
            var value = test.sut2.AddressId;
            var sut = AddressHelper.GetAddressInfoById(sut2);

            Assert.Equal(value, sut.AddressId);
        }
    };

    }

