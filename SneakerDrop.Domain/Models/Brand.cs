using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SneakerDrop.Domain.Models
{
    [Table("Brand", Schema = "Product")]
    public class Brand
    {
       
        public int BrandId { get; set; }


        [StringLength(50)]
        [Required]
        public string BrandName { get; set; }
    }
}
