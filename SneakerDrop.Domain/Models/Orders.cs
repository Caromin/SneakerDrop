﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SneakerDrop.Domain.Models
{
    [Table("Orders", Schema = "User")]
    public class Orders
    {
        [Key]
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

        public Payment PaymentId { get; set; }

        public User UserId { get; set; }

    }
}
