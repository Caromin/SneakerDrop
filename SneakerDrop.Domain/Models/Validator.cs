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
            var validateEmail = user.Email;

            Match match = Regex.Match(validateEmail, pattern);

            if (match.Success)
            {
                return true;
            }
            return false;
        }

        public bool ValidateStreet(Address address)
        {
            string pattern = @"^[A-Za-z0-9]+(?:\s[A-Za-z0-9'_-]+)+$";
            var validate = address.Street;

            string pattern2 = @"^[0-9]+$";
            var validatePostalCode = address.PostalCode;

            Match match = Regex.Match(validate, pattern);
            Match match2 = Regex.Match(validatePostalCode, pattern2);

            if (match.Success && match2.Success)
            {
                return true;
            }
            return false;
        }

        public bool EditExistingUser(User userModel)
        {
            string editUser = @"^[a-zA-Z0-9]+$";
            Match match = Regex.Match(userModel.Username, editUser);

            if (match.Success)
            {
                return true;
            }
            return false;
        }

        public bool ValidateNewPayment(Payment paymentView)
        {
            //var ccNumberValidation = new Regex(@"^[0-9]{16}$");
            //var monthCheck = new Regex(@"^(0[1-9]|1[0-2])$");
            //var yearCheck = new Regex(@"^20[0-9]{2}$");
            //var cvvCheck = new Regex(@"^\d{3}$");
            //var ccUserNameCheck = new Regex(@"^[a-zA-Z]+$");


            //if (ccNumberValidation.IsMatch(paymentView.CCNumber.ToString()) &&
            //    cvvCheck.IsMatch(paymentView.CVV.ToString()) &&
            //    ccUserNameCheck.IsMatch(paymentView.CCUserName))
            //{
            //    return true;
            //}

            return true;
        }
        public bool ValidateProductTitle(ProductInfo productInfoDomainModel)
        {
            var productTitleCheck = new Regex(@"^[a-zA-Z0-9]+$");

            if (productTitleCheck.IsMatch(productInfoDomainModel.ProductTitle))
            {
                return true;
            }
            return false;
        }

    }
}
