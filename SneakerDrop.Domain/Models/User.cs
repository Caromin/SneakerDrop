using System;
using SneakerDrop.Domain.Interfaces;

namespace SneakerDrop.Domain.Models
{
    public class User : IUser
    {
        public int UserId { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool CheckDbForUser(User login)
        {
            return true;
        }

        public bool ValidateString(User word)
        {
            return true;
        }
    }
}
