using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace SneakerDrop.Domain.Models
{
    public class Validator
    {
        public bool ValidateString(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password) || string.IsNullOrWhiteSpace(user.Email))
            {
                return true;
            }

            return false;
        }

       public bool ValidateUsername(User user)
        {
            string pattern = @"^[a-zA-Z0-9]+$";
            var username = user.Username;

            Match match = Regex.Match(username, pattern);
            if (match.Success)
            {
                return true;
            }
            return false;
            
        }
    }
}
