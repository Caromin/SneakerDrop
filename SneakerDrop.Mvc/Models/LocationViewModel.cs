using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using SneakerDrop.Domain.Models;

namespace SneakerDrop.Mvc.Models
{
    public class LocationViewModel
    {
        public int AddressId { get; set; }

        [StringLength(50)]
        [Required]
        public string Street { get; set; }

        [StringLength(50)]
        [Required]
        public string City { get; set; }

        [StringLength(50)]
        [Required]
        public string State { get; set; }

        [StringLength(50)]
        [Required]
        public string PostalCode { get; set; }
        public User User { get; set; }
    }
}
