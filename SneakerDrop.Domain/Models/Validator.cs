using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
    }
}
