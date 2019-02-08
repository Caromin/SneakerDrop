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
