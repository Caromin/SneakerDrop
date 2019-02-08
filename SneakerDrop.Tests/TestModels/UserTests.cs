using System;
using AutoMapper;
using SneakerDrop.Code.Helpers;
using SneakerDrop.Domain.Models;
using SneakerDrop.Mvc.AutoMapperModels;
using SneakerDrop.Mvc.Models;
using Xunit;

namespace SneakerDrop.Tests.TestModels
{
    public class UserTests
    {
        public User user = new User
        {
            UserId = 1,
            Username = "ian25192",
            Password = "Password",
            Firstname = "Ian",
            Lastname = "Nai",
            Email = "Email@email.com"
        };

        [Fact(Skip = "IsWorking")]
        public void Test_AddUser()
        {
            var sut = UserHelper.AddUser(user);

            Assert.True(sut);
        }

        //[Fact]
        //public void Test_GetUserInfoById()
        //{
        //    var test = new UserTests();
        //    var value = test.user.Firstname;
        //    var sut = UserHelper.GetUserInfoById(user);

        //    Assert.Equal(value, sut.Firstname);
        //}
        [Fact]
        public void Test_EditUserInfoById()
        {
            var sut = UserHelper.EditUserInfoById(user);

            Assert.True(sut);

        }
        [Fact]
        public void Test_UserMapper()
        {

            var config = AutoMapperProfile.userMapper;
            
            config.AssertConfigurationIsValid();

            Assert.NotNull(config);

        }
    }
}
