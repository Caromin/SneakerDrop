using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SneakerDrop.Domain.Models
{
    [Table("Orders", Schema = "User")]
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }

        [ForeignKey("Listing")]
        [Required]
        public Listing ListingId { get; set; }

        [ForeignKey("User")]
        [Required]
        public User UserId { get; set; }

        [ForeignKey("Payment")]
        [Required]
        public Payment PaymentId { get; set; }

        [Required]
        public int OrderGroupNumber { get; set; }

        [Required]
        public int Quantity { get; set; }

        [StringLength(50)]
        [Required]
        public string ShippingStatus { get; set; }

        [Timestamp]
        public DateTime Timestamp { get; set; }

        public bool AddOrderByUser()
        {
            return true;
        }

        public bool GetOrderByUser()
        {
            return true;
        }

        public bool DeleteOrderByUser()
        {
            return true;
        }
    }
}
