using AutoMapper;
using sdm = SneakerDrop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SneakerDrop.Data2.Helpers
{
     public static class DomainHelper
    {
        public static MapperConfiguration userMapper = new MapperConfiguration (mc => mc.CreateMap<User>)
    }
}
