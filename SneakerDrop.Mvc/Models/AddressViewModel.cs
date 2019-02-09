using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using SneakerDrop.Code.Helpers;
using dm = SneakerDrop.Domain.Models;

namespace SneakerDrop.Mvc.Models
{
    public class AddressViewModel
    {
        public int AddressId { get; set; }

        public string HelperType { get; set; }

        [StringLength(50)]
        [Required]
        public string Street { get; set; }

        [StringLength(50)]
        [Required]
        public string City { get; set; }

        [StringLength(50)]
        [Required]
        public string State { get; set; }

        [StringLength(50)]
        [Required]
        public string PostalCode { get; set; }

        public int UserId { get; set; }

        public List<AddressViewModel> AddressValidator(AddressViewModel addressView)
        {
            var createModel = new ConversionAddress();
            var validator = new dm.Validator();
            dm.Address addressDomainModel = createModel.MappingAddress(addressView);

            //validator info methods
            // if statement
            switch (addressView.HelperType)
            {
                case "Add":
                    var valCheckAdd = validator.ValidateNewAdddress(addressDomainModel);

                    if (valCheckAdd)
                    {
                        AddressHelper.AddAddressById(addressDomainModel);
                        return null;
                    }
                    // write error method call here
                    return null;
                case "Get":
                    List<dm.Address> domainAddressList = AddressHelper.GetAddressInfoById(addressDomainModel);
                    List<AddressViewModel> viewAddressList = createModel.MappingView(domainAddressList);

                    return viewAddressList;
                case "Edit":
                    AddressHelper.EditAddressInfoById(addressDomainModel);
                    return null;
                case "Delete":
                    AddressHelper.DeleteAddressInfoById(addressDomainModel);
                    return null;
                default:
                    return null;
            }
        }
    }

    public class ConversionAddress : Profile
    {
        public static MapperConfiguration addressConfig = new MapperConfiguration(cgf => cgf.CreateMap<AddressViewModel, dm.Address>()
            .ForMember(a => a.AddressId, av => av.MapFrom(src => src.AddressId))
            .ForMember(a => a.Street, av => av.MapFrom(src => src.Street))
            .ForMember(a => a.City, av => av.MapFrom(src => src.City))
            .ForMember(a => a.State, av => av.MapFrom(src => src.State))
            .ForMember(a => a.PostalCode, av => av.MapFrom(src => src.PostalCode))
            .ForMember(a => a.User.UserId, av => av.MapFrom(src => src.UserId)));

        public static MapperConfiguration viewConfig = new MapperConfiguration(cgf => cgf.CreateMap<dm.Address, AddressViewModel>()
            .ForMember(a => a.AddressId, av => av.MapFrom(src => src.AddressId))
            .ForMember(a => a.Street, av => av.MapFrom(src => src.Street))
            .ForMember(a => a.City, av => av.MapFrom(src => src.City))
            .ForMember(a => a.State, av => av.MapFrom(src => src.State))
            .ForMember(a => a.PostalCode, av => av.MapFrom(src => src.PostalCode))
            .ForMember(a => a.UserId, av => av.MapFrom(src => src.User.UserId)));

        public dm.Address MappingAddress(AddressViewModel addressView)
        {
            var addressMapper = addressConfig.CreateMapper();

            return addressMapper.Map<AddressViewModel, dm.Address>(addressView);
        }

        public List<AddressViewModel> MappingView(List<dm.Address> domainAddressList)
        {
            var addressModel = viewConfig.CreateMapper();

            List<AddressViewModel> convertedList = new List<AddressViewModel>();

            foreach (var item in domainAddressList)
            {
                var newItem = addressModel.Map<dm.Address, AddressViewModel>(item);
                convertedList.Add(newItem);
            }
            return convertedList;
        }
    }
}
