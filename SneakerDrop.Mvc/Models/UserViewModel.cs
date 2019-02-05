using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dm = SneakerDrop.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SneakerDrop.Mvc.Models
{
    public class UserViewModel
    {
        public IEnumerable<dm.User> Users { get; set; }

        public IEnumerable<dm.Address> Addresses { get; set; }


        [StringLength(50)]
        [Required]
        public string Firstname { get; set; }

        [StringLength(50)]
        [Required]
        public string Lastname { get; set; }

        [StringLength(50)]
        [Required]
        public string Username { get; set; }

        [StringLength(50)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(50)]
        [Required]
        public string Password { get; set; }
    }
}
