using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SneakerDrop.Domain.Models
{
    public class Listing
    {
        [Key]
        public int ListingId { get; set; }

        [ForeignKey("User")]
        [Required]
        public User UserId { get; set; }

        [ForeignKey("ProductInfo")]
        [Required]
        public ProductInfo ProductId { get; set; }

        [Required]
        public decimal UserSetPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        [StringLength(50)]
        [Required]
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
