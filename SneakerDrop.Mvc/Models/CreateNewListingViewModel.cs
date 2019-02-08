using System;
using System.ComponentModel.DataAnnotations;

namespace SneakerDrop.Mvc.Models
{
    public class CreateNewListingViewModel
    {
        public int ListingId { get; set; }

        [Required]
        public decimal UserSetPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        [StringLength(50)]
        [Required]
        public string Size { get; set; }

        public int UserId { get; set; }

        public int ProductInfoId { get; set; }

        public int BrandId { get; set; }

        [StringLength(50)]
        [Required]
        public string BrandName { get; set; }

        public int TypeId { get; set; }

        [StringLength(50)]
        [Required]
        public string TypeName { get; set; }

        [StringLength(50)]
        [Required]
        public string ProductTitle { get; set; }

        [StringLength(500)]
        [Required]
        public string Description { get; set; }

        [Required]
        public int DisplayPrice { get; set; }

        [StringLength(50)]
        [Required]
        public string ReleaseDate { get; set; }

        [StringLength(50)]
        [Required]
        public string Color { get; set; }

    }
}
