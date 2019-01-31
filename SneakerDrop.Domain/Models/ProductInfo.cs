﻿using System;
using System.ComponentModel.DataAnnotations;

namespace SneakerDrop.Domain.Models
{
    public class ProductInfo
    {
        [Key]
        public int ProductInfoId { get; set; }

        [StringLength(50)]
        [Required]
        public string ProductTitle { get; set; }

        [StringLength(50)]
        [Required]
        public string Brand { get; set; }

        [StringLength(50)]
        [Required]
        public string Type { get; set; }

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