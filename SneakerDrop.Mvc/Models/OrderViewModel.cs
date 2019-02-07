using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SneakerDrop.Mvc.Models
{
    public class OrderViewModel
    {

        public int OrderId { get; set; }

        [Required]
        public int OrderGroupNumber { get; set; }

        [Required]
        public int OrderQuantity { get; set; }

        [StringLength(50)]
        [Required]
        public string ShippingStatus { get; set; }

        [Timestamp]
        public DateTime Timestamp { get; set; }

 



    }
}
