using System;
using SneakerDrop.Domain.Models;

namespace SneakerDrop.Tests.TestModels
{
    public class UserTests
    {

        public void Test_CheckDbForUser()
        {
            var sut = new User
            {
                Username = "ian2519",
                Password = "password1"
            };

        }
    }
}
