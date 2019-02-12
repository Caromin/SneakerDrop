using SneakerDrop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SneakerDrop.Code.Helpers
{
    public static class UserHelper
    {
        private static SneakerDropDbContext _db = new SneakerDropDbContext();

        public static bool AddUser(User user)
        {
            _db.Users.Add(user);
            return _db.SaveChanges() == 1;

        }
        public static User GetUserInfoByUsername(User user)
        {
            User dbInfo = _db.Users.Where(u => u.Username == user.Username && u.Password == user.Password).FirstOrDefault();

            return dbInfo;
              
        }
        public static User GetUserInfoByIdForAddress(Address address)
        {
            User dbInfo = _db.Users.Where(u => u.UserId == address.User.UserId).FirstOrDefault();

            return dbInfo;
        }
        public static User GetUserInfoByIdForPayment(Payment payment)
        {
            User dbInfo = _db.Users.Where(u => u.UserId == payment.User.UserId).FirstOrDefault();

            return dbInfo;
        }

        public static List<User> GetAllUsers()
        {
            var datauser = _db.Users.ToList();
            return datauser;
        }

        public static bool EditUserInfoById(User user)
        {
            var results = _db.Users.Where(u => u.UserId == user.UserId).FirstOrDefault();

            results.Username = user.Username;
            results.Firstname = user.Firstname;
            results.Lastname = user.Lastname;
            results.Password = user.Password;
            results.Email = user.Email;

            _db.SaveChanges();

            return true;
        }
    }

}
