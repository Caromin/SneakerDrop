using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SneakerDrop.Mvc.Models
{
    public class PaymentViewModel
    {
        public int PaymentId { get; set; }

        [Required]
        public long CCNumber { get; set; }

        [StringLength(50)]
        [Required]
        public string CCUserName { get; set; }

        [Required]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int CVV { get; set; }
    }
}
