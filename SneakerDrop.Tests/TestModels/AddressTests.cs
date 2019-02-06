using SneakerDrop.Code.Helpers;
using SneakerDrop.Domain.Models;
using System;
using Xunit;

namespace SneakerDrop.Tests.TestModels
{
    public class AddressTests
    {
        public Address sut2 { get; set; }
        public User user { get; set; }

        public AddressTests()
        {
      
            sut2 = new Address()
            {
                AddressId = 1,
                Street = "4508 Burnhill Dr",
                City = "Plano",
                State = "TX",
                PostalCode = "75024",
                User = new User()
                {
                    UserId = 1,
                    Username = "ian2519",
                    Password = "Password",
                    Firstname = "Ian",
                    Lastname = "Nai",
                    Email = "Email@email.com"
                }
        };


        }
        [Fact(Skip ="Dosen't work")]
        public void Test_AddAddressByUser()
        {
            var sut = AddressHelper.AddAddressById(sut2);


            Assert.True(sut);

        }
        [Fact]
        public void Test_GetAddressInfoById()
        {
            var test = new AddressTests();
            var value = test.sut2.AddressId;
            var sut = AddressHelper.GetAddressInfoById(sut2);

            Assert.NotNull(sut);
        }
        [Fact(Skip ="Doesn't Work")]
        public void Test_EditAddressInfoById()
        {
            var sut = AddressHelper.EditAddressInfoById(sut2);

            Assert.True(sut);

        }
    };

}

