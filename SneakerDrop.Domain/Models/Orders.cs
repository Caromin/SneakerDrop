using System;
namespace SneakerDrop.Domain.Models
{
    public class Orders
    {
        public int OrderId { get; set; }

        public Listing ListingId { get; set; }

        public User UserId { get; set; }

        public Payment PaymentId { get; set; }

        public DateTime Timestamp { get; set; }

        public int OrderGroupNumber { get; set; }

        public int Quantity { get; set; }

        public string ShippingStatus { get; set; }

        public bool AddOrderByUser()
        {
            return true;
        }

        public bool GetOrderByUser()
        {
            return true;
        }

        public bool DeleteOrderByUser()
        {
            return true;
        }
    }
}
