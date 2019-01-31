using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SneakerDrop.Domain.Interfaces;

namespace SneakerDrop.Domain.Models
{
    [Table("User", Schema = "User")]
    public class User : IUser
    {
       
        public int UserId { get; set; }

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
        public string Email { get; set; }

        [StringLength(50)]
        [Required]
        public string Password { get; set; }
    }
}
