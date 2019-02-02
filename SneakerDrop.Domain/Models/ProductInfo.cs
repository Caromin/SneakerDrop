using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SneakerDrop.Domain.Models
{
    [Table("Product", Schema = "Store")]
    public class ProductInfo
    {
      
        public int ProductInfoId { get; set; }

        public Brand Brand { get; set; }


        public Type Type { get; set; }

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

        public bool CheckString()
        {
            return Validator.ValidateString(this) && Validator.ValidateNumber(this);
        }
    }
}
