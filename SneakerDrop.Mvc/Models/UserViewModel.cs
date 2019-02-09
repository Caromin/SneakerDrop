﻿using dm = SneakerDrop.Domain.Models;
using System.ComponentModel.DataAnnotations;
using SneakerDrop.Code.Helpers;
using AutoMapper;

namespace SneakerDrop.Mvc.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }

        public string HelperType { get; set; }

        [StringLength(50)]
        [Required]
        public string Firstname { get; set; }

        [StringLength(50)]
        [Required]
        public string Lastname { get; set; }

        [StringLength(50)]
        [MinLength(5)]
        [MaxLength(15)]
        [Required]
        public string Username { get; set; }

        [StringLength(50)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(50)]
        [MinLength(5)]
        [MaxLength(15)]
        [Required]
        public string Password { get; set; }

        public UserViewModel UserValidator(UserViewModel userView)
        {
            var createModel = new ConversionUser();
            var validator = new dm.Validator();
            dm.User userModel = createModel.MappingUser(userView);

            switch (userView.HelperType)
            {
                case "Add":
                    var valCheckAdd = validator.ValidateNewUser(userModel);

                    if (valCheckAdd)
                    {
                        UserHelper.AddUser(userModel);
                    }
                    // write error method call here
                    return null;
                case "Get":
                    var valCheckUsername = validator.ValidateUserName(userModel);

                    if (valCheckUsername)
                    {
                        var userInfo = UserHelper.GetUserInfoById(userModel);
                        return createModel.MappingViewInfo(userInfo);
                    }
                    break;
                case "Edit":
                    var valCheckEdit = validator.EditString(userModel);

                    if (valCheckEdit)
                    {
                        UserHelper.EditUserInfoById(userModel);
                    }
                    // write error method call here
                    return null;
                default:
                    return null;
            }

            return null;
        }
    }

    public class ConversionUser : Profile
    {
        public static MapperConfiguration userConfig = new MapperConfiguration(cfg => cfg.CreateMap<UserViewModel, dm.User>()
           .ForMember(d => d.UserId, v => v.MapFrom(src => src.UserId))
           .ForMember(d => d.Firstname, v => v.MapFrom(src => src.Firstname))
           .ForMember(d => d.Lastname, v => v.MapFrom(src => src.Lastname))
           .ForMember(d => d.Username, v => v.MapFrom(src => src.Username))
           .ForMember(d => d.Email, v => v.MapFrom(src => src.Email))
           .ForMember(d => d.Password, v => v.MapFrom(src => src.Password)));

        public static MapperConfiguration viewConfig = new MapperConfiguration(cfg => cfg.CreateMap<dm.User, UserViewModel>()
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

        public UserViewModel MappingViewInfo(dm.User user)
        {
            var userViewMapper = viewConfig.CreateMapper();

            return userViewMapper.Map<dm.User, UserViewModel>(user);
        }

    }
}
