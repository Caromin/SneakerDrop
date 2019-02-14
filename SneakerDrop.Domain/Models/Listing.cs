using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SneakerDrop.Code.Helpers;

namespace SneakerDrop.Domain.Models
{
    [Table("Listing", Schema = "Store")]
    public class Listing
    {

        public int ListingId { get; set; }

        [Required]
        public decimal UserSetPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        [StringLength(50)]
        [Required]
        public string Size { get; set; }

        public User User { get; set; }

        public ProductInfo ProductInfo { get; set; }

        public bool AddListing(Listing listing)
        {
            // add validation here before sending to db
            ListingHelper.AddListingById(listing);

            return true;
        }

        public static decimal CartTotal(Listing productinfo, Listing buyerinfo)
        {
            decimal PendingPrice = 0;

            if (buyerinfo.Quantity > productinfo.Quantity || buyerinfo.Quantity < 0)
            {
                PendingPrice = -1;
                return PendingPrice;
            }
            PendingPrice = 1;
            return PendingPrice;

        }
    }
}
