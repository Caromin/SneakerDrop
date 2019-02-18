using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SneakerDrop.Domain.Models
{
    [Table("Address", Schema = "User")]
    public class Address
    {

        public int AddressId { get; set; }

        [StringLength(50)]
        public string Street { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [StringLength(50)]
        public string PostalCode { get; set; }

        public int DefaultAddress { get; set; }

        public User User { get; set; }
    }
}
