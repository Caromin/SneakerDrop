using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SneakerDrop.Domain.Models
{
    public class Validator
    {
        public bool ValidateString(string username, string password, string email)
        { 
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email))
            {
                return true;
            }

            return false;
        }
    }
}
