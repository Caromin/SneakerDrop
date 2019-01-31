using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    }
}
