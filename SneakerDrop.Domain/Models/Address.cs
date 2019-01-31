using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SneakerDrop.Domain.Models
{
    [Table("Address", Schema = "User")]
    public class Address
    {

        [Key]
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

        public User UserId { get; set; }
    }
}
