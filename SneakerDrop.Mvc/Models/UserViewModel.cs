using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dm = SneakerDrop.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SneakerDrop.Code.Helpers;
using AutoMapper;

namespace SneakerDrop.Mvc.Models
{
    public class UserViewModel
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
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(50)]
        [Required]
        public string Password { get; set; }

        public void LoginValidator(UserViewModel userView)
        {
            var loginModel = new ConvertToDomainUser();
            var userModel = loginModel.MappingUser(userView);

            var validator = new dm.Validator();
            var valCheck = validator.ValidateString(userModel);

            if (valCheck)
            {
                UserHelper.GetUserInfoById(userModel);
            }
        }
    }

    public class ConvertToDomainUser : Profile
    {
        public static MapperConfiguration userConfig = new MapperConfiguration(cfg => cfg.CreateMap<UserViewModel, dm.User>()
           .ForMember(d => d.UserId, v => v.MapFrom(src => src.UserId))
           .ForMember(d => d.Firstname, v => v.MapFrom(src => src.Firstname))
           .ForMember(d => d.Lastname, v => v.MapFrom(src => src.Lastname))
           .ForMember(d => d.Username, v => v.MapFrom(src => src.Username))
           .ForMember(d => d.Email, v => v.MapFrom(src => src.Email))
           .ForMember(d => d.Password, v => v.MapFrom(src => src.Password)));

        public dm.User MappingUser(UserViewModel userView)
        {
            var userMapper = userConfig.CreateMapper();

            return userMapper.Map<UserViewModel, dm.User>(userView);
        }
    }
}
