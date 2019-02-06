using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dm = SneakerDrop.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SneakerDrop.Code.Helpers;

namespace SneakerDrop.Mvc.Models
{
    public class UserViewModel
    {

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

        public void LoginValidator()
        {
            var validator = new dm.Validator();
            var valCheck =  validator.ValidateString(Username, Password, Email);
            
            if (valCheck)
            {
                // waiting for automapper to match models
                // UserHelper.GetUserInfoById(user);
            }
        }
    }
}
