using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using dm = SneakerDrop.Domain.Models;
using SneakerDrop.Mvc.Models;

namespace SneakerDrop.Mvc.AutoMapperModels
{
    public class AutoMapperProfile : Profile
    {
        public static MapperConfiguration userMapper = new MapperConfiguration(mc =>
        {
            mc.CreateMap<UserViewModel, dm.User>()
            .ForMember(m => m.UserId, u => u.MapFrom(src => src.UserId))
            .ForMember(m => m.Username, u => u.MapFrom(src => src.Username))
            .ForMember(m => m.Password, u => u.MapFrom(src => src.Password))
            .ForMember(m => m.Firstname, u => u.MapFrom(src => src.Firstname))
            .ForMember(m => m.Lastname, u => u.MapFrom(src => src.Lastname))
            .ForMember(m => m.Email, u => u.MapFrom(src => src.Email));
        });

    }
}