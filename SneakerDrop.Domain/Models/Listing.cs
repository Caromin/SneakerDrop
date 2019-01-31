using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SneakerDrop.Domain.Models
{
    [Table("Listing", Schema = "Store")]
    public class Listing
    {
        [Key]
        public int ListingId { get; set; }

        [Required]
        public decimal UserSetPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        [StringLength(50)]
        [Required]
        public string Size { get; set; }

        public Orders Orders { get; set; }

        public User UserId { get; set; }

        public ProductInfo ProductInfoId { get; set; }
    }
}
