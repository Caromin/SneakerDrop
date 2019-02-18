using SneakerDrop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SneakerDrop.Tests.TestModels
{
    public class ValidationTests
    {
        Validator valid = new Validator();
        [Fact]
        public void Test_ValidateString()
        {
            User user = new User
            {
                Username = "nick"
            };

            var sut = valid.ValidateString(user);

            Assert.True(sut);
        }
        [Fact]
        public void Test_ValidateEmail()
        {
            User user = new User
            {
                Email = "nick@email.com"
            };

            var sut = valid.ValidateEmail(user);

            Assert.True(sut);
        }
        [Fact]
        public void Test_ValidateUserName()
        {
            User user = new User
            {
                Username = "johndoe123"
            };

            var sut = valid.ValidateUserName(user);

            Assert.True(sut);
        }
        [Fact]
        public void Test_ValidateStreet()
        {
            Address address = new Address
            {
                Street = "1234 Fake St",
                PostalCode = "75024"
            };

            var sut = valid.ValidateStreet(address);

            Assert.True(sut);
        }
        [Fact]
        public void Test_EditExistingUser()
        {
            User user = new User
            {
                Username = "bob123"
            };

            var sut = valid.EditExistingUser(user);

            Assert.True(sut);
        }
        [Fact]
        public void Test_ValidateProductTitle()
        {
            ProductInfo product = new ProductInfo
            {
                ProductTitle = "Yeezy"
            };

            var sut = valid.ValidateProductTitle(product);

            Assert.True(sut);
        }
        [Fact]
        public void Test_ValidateShoeSize()
        {
            Listing listing = new Listing
            {
                Size = "5"
            };
            var sut = valid.ValidateShoeSize(listing);

            Assert.True(sut);
        }
    }
}
