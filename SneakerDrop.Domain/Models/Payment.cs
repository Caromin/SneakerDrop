using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SneakerDrop.Domain.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [ForeignKey("User")]
        [Required]
        public User UserId { get; set; }

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

        public bool AddPaymentByUser()
        {
            return true;
        }

        public bool GetPaymentByUser()
        {
            return true;
        }

        public bool EditPaymentByUser()
        {
            return true;
        }

        public bool DeletePaymentByUser()
        {
            return true;
        }
    }
}
