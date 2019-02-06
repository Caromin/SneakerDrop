using SneakerDrop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SneakerDrop.Code.Helpers
{
    public class UserHelper
    {
        private static SneakerDropDbContext _db = new SneakerDropDbContext();

        public static bool AddUser(User user)
        {
            _db.Users.Add(user);
            return _db.SaveChanges() == 1;
     
        }
        public User GetUserInfoById(User user)
        {
           User dbInfo = _db.Users.Where(u => u.UserId == user.UserId).FirstOrDefault();

            return dbInfo;
        }
        public List<User> GetAllUsers()
        {
            var getallusers = _db.Users.ToList();
            return getallusers;
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
