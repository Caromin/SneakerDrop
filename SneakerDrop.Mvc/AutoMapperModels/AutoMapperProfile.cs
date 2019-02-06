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
        public AutoMapperProfile()
        {
            CreateMap<dm.User, UserViewModel>().ReverseMap();
        }

    }
}