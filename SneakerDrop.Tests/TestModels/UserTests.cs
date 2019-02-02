using System;
using SneakerDrop.Code.Helpers;
using SneakerDrop.Domain.Models;
using Xunit;

namespace SneakerDrop.Tests.TestModels
{
    public class UserTests
    {
        public User user = new User
        {
            Username = "ian2519",
            Password = "Password",
            Firstname = "Ian",
            Lastname = "Nai",
            Email = "Email@email.com"
        };

        [Fact]
        public void Test_AddUser()
        {
            var sut = UserHelper.AddUser(user);

            Assert.True(sut);   
        }

    }
}
