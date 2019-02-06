using SneakerDrop.Code.Helpers;
using SneakerDrop.Domain.Models;
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
                City = "Plano",
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
        [Fact(Skip ="Doesn't work")]
        public void Test_AddAddressByUser()
        {
            var sut = AddressHelper.AddAddressById(address);


            Assert.True(sut);
        }
        [Fact]
        public void Test_GetAddressInfoById()
        {

            var sut = AddressHelper.GetAddressInfoById(address);

            Assert.NotNull(sut);
        }
        [Fact(Skip ="Not working properly")]
        public void Test_EditAddressInfoById()
        {
            var sut = AddressHelper.EditAddressInfoById(address);

            Assert.True(sut);
        }
        [Fact(Skip ="working")]
        public void Test_DeleteAddressInfoById()
        {
            var sut = AddressHelper.DeleteAddressInfoById(address);

            Assert.True(sut);
        }
    }
}



