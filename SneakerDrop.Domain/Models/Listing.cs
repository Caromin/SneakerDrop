using System;
namespace SneakerDrop.Domain.Models
{
    public class Listing
    {
        public int ListingId { get; set; }

        public User UserId { get; set; }

        public ProductInfo ProductId { get; set; }

        public decimal UserSetPrice { get; set; }

        public int Quantity { get; set; }

        public string Size { get; set; }

        public bool AddListingByUser()
        {
            return true;
        }

        public bool GetListingByUser()
        {
            return true;
        }

        public bool EditListingByUser()
        {
            return true;
        }

        public bool DeleteListingByUser()
        {
            return true;
        }

        public bool GetProductInfoByProductId()
        {
            return true;
        }
    }
}
