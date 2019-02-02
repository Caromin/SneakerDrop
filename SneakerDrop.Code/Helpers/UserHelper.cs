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
        public static User GetUserInfoById(User user)
        {
           User dbInfo = _db.Users.Where(u => u.UserId == user.UserId).FirstOrDefault();

            //var dbUser = new User
            //{
            //    UserId = dbInfo.UserId,
            //    Username = dbInfo.Username,
            //    Firstname = dbInfo.Firstname,
            //    Lastname = dbInfo.Lastname,
            //    Email = dbInfo.Email,
            //    Password = dbInfo.Password
            //};

            return dbInfo;
        }

    }

}
