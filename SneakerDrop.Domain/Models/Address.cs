using System;
namespace SneakerDrop.Domain.Models
{
    public class Address
    {
        public int AddressId { get; set; }

        public User UserId { get; set; }

        public long CCNumber { get; set; }

        public string CCUserName { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public int CVV { get; set; }

        public bool AddAddressByUser()
        {
            return true;
        }

        public bool GetAddressByUser()
        {
            return true;
        }

        public bool EditAddressByUser()
        {
            return true;
        }

        public bool DeleteAddressByUser()
        {
            return true;
        }
    }
}
