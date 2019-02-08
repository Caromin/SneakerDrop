using System;
using System.ComponentModel.DataAnnotations;

namespace SneakerDrop.Mvc.Models
{
    public class AddressViewModel
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

        public int UserId { get; set; }
    }
}
