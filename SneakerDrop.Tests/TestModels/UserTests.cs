﻿using System;
using AutoMapper;
using SneakerDrop.Code.Helpers;
using SneakerDrop.Domain.Models;
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

        [Fact]
        public void Test_GetUserInfoById()
        {
            var test = new UserTests();
            var value = test.user.Username;
            var sut = UserHelper.GetUserInfoByUsername(user);

            Assert.Equal(value, sut.Username);
        }

        [Fact]
        public void Test_EditUserInfoById()
        {
            var sut = UserHelper.EditUserInfoById(user);

            Assert.True(sut);

        }
        [Fact]
        public void Test_LoginValidator()
        {
            var sut = new UserViewModel
            {
                UserId = 76,
                Username = "SimonPegg",
                Password = "HazzyMax312",
                Firstname = "Kobe",
                Lastname = "Bryant",
                Email = "torch@email.com"
            };

            var test = sut.LoginValidator(sut);

            Assert.Equal(sut.Firstname, test.Firstname);
        }
        [Fact]
        public void Test_AddEditUser()
        {
            var sutAdd = new UserViewModel
            {
                HelperType = "add",
                Username = "ian25192",
                Password = "Password",
                Firstname = "Bob",
                Lastname = "Jan",
                Email = "Email@email.com"
            };
            var test = sutAdd.AddEditUser(sutAdd);

            Assert.True(test);
        }     
        [Fact(Skip ="needs working on")]
        public void Test_EmailValidator()
        {
            var sut = new UserViewModel
            {
                Email = "email@email.com"
            };
            var test = sut.EmailValidator(sut);

            Assert.True(test);
        }
        [Fact]
        public void Test_GetUserInfoByUsername()
        {
            var sut = UserHelper.GetUserInfoByUsername(user);

            Assert.Equal(sut.Username, user.Username);
        }
        [Fact]
        public void Test_getUserInfoByIdForAddres()
        {
            Address address = new Address
            {
                User = new User
                {
                    UserId = 1
                }
            };
            var sut = UserHelper.GetUserInfoByIdForAddress(address);

            Assert.Equal(sut.UserId, user.UserId);
        }
        [Fact]
        public void Test_GetUserInfoByIdForPayment()
        {
            Payment payment = new Payment
            {
                User = new User
                {
                    UserId = 1
                }
            };
            var sut = UserHelper.GetUserInfoByIdForPayment(payment);

            Assert.Equal(sut.UserId, user.UserId);
        }
        [Fact]
        public void Test_GetAllUsers()
        {
            var sut = UserHelper.GetAllUsers();

            Assert.NotEmpty(sut);
        }
    }
}
