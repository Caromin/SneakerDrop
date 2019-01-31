using System;
namespace SneakerDrop.Domain.Models
{
    public class Payment
    {
        public int AddressId { get; set; }

        public User UserId { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public bool AddPaymentByUser()
        {
            return true;
        }

        public bool GetPaymentByUser()
        {
            return true;
        }

        public bool EditPaymentByUser()
        {
            return true;
        }

        public bool DeletePaymentByUser()
        {
            return true;
        }
    }
}
