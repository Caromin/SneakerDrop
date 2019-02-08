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
        public bool ValidateUserName(User user)
        {
            string pattern = @"^[a-zA-Z0-9]+$";
            var validate = user.Username;
            Match match = Regex.Match(validate, pattern);

            if (match.Success)
            {
                return true;
            }

            return false;
        }
        public bool ValidateEmail(User user)
        {
            string pattern = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            var validate = user.Email;
            Match match = Regex.Match(validate, pattern);

            if (match.Success)
            {
                return true;
            }
            return false;
        }

        public bool ValidateNewUser(User userModel)
        {
            return true;
        }

        public bool EditString(User userModel)
        {
            return true;
        }

        public bool ValidateNewPayment(Payment paymentView)
        {
            return true;
        }

        public bool ValidateProductTitle(ProductInfo productInfoDomainModel)
        {
            return true;
        }
    }
}
