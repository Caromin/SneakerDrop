using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SneakerDrop.Domain.Interfaces;

namespace SneakerDrop.Domain.Models
{
    [Table("User", Schema = "User")]
    public class User : IUser
    {
        [Key]
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

        public Address Address { get; set; }

        public Orders Orders { get; set; }

        public Payment Payment { get; set; }

        public Listing  Listing { get; set; }


        public bool CheckDbForUser(User login)
        {
            return true;
        }

        public bool ValidateString(User word)
        {
            return true;
        }
    }
}
