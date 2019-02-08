using System;
using System.ComponentModel.DataAnnotations;

namespace SneakerDrop.Mvc.Models
{
    public class OrderAndPaymentViewModel
    {
        public int OrderId { get; set; }

        [Required]
        public int OrderGroupNumber { get; set; }

        [Required]
        public int Quantity { get; set; }

        [StringLength(50)]
        [Required]
        public string ShippingStatus { get; set; }

        [Timestamp]
        public DateTime Timestamp { get; set; }

        [StringLength(50)]
        [Required]
        public string Size { get; set; }

        public int ListingId { get; set; }

        [Required]
        public decimal UserSetPrice { get; set; }

        public int PaymentId { get; set; }

        public int UserId { get; set; }
    }
}
