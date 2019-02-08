using System;
using System.ComponentModel.DataAnnotations;
using SneakerDrop.Domain.Models;

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

        public User User { get; set; }
    }
}
