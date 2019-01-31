using System;
using SneakerDrop.Domain.Models;
using Xunit;

namespace SneakerDrop.Tests.TestModels
{
    public class UserTests
    {
        public UserTests()
        {
            var sut = new User
            {
                Username = "ian2519",
                Password = "Password",
                Firstname = "Ian",
                Lastname = "Nai",
                Email = "Email@email.com"
            };
        }

        [Fact]
        public void Test_LoginValidation()
        {
            su
            
        }
    }
}
