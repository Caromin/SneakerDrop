using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using SneakerDrop.Code.Helpers;
using SneakerDrop.Domain.Models;
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

        public ConversionAddress createModel = new ConversionAddress();

        public dm.Validator validator = new dm.Validator();

        public List<AddressViewModel> GetAllAddresses(AddressViewModel addressView)
        {

            dm.Address addressDomainModel = createModel.MappingAddress(addressView);
            List<dm.Address> domainAddressList = AddressHelper.GetAddressInfoById(addressDomainModel);
            List<AddressViewModel> viewAddressList = createModel.MappingView(domainAddressList);

            return viewAddressList;
        }

        public bool AddEditDeleteAddresses(AddressViewModel addressView)
        {
            dm.Address addressDomainModel = createModel.MappingAddress(addressView);
            // gets userinfo for edit
            dm.User getUser = UserHelper.GetUserInfoByIdForAddress(addressDomainModel);
            var valCheckAdd = validator.ValidateStreet(addressDomainModel);

            if (addressView.HelperType == "add")
            {
                if (valCheckAdd)
                {
                    var addedAddress = new dm.Address
                    {
                        AddressId = addressDomainModel.AddressId,
                        Street = addressDomainModel.Street,
                        City = addressDomainModel.City,
                        State = addressDomainModel.State,
                        PostalCode = addressDomainModel.PostalCode,
                        User = new User
                        {
                            UserId = getUser.UserId,
                            Username = getUser.Username,
                            Password = getUser.Password,
                            Firstname = getUser.Firstname,
                            Lastname = getUser.Lastname,
                            Email = getUser.Email
                        }
                    };

                    AddressHelper.AddAddressById(addedAddress);
                    return true;
                }
                return false;
            }
            var editedAddress = new dm.Address
            {
                AddressId = addressDomainModel.AddressId,
                Street = addressDomainModel.Street,
                City = addressDomainModel.City,
                State = addressDomainModel.State,
                PostalCode = addressDomainModel.PostalCode,

                User = new User
                {
                    UserId = getUser.UserId,
                    Username = getUser.Username,
                    Password = getUser.Password,
                    Firstname = getUser.Firstname,
                    Lastname = getUser.Lastname,
                    Email = getUser.Email
                }
            };

            AddressHelper.EditAddressInfoById(editedAddress);
            return true;
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
            .ForPath(a => a.User.UserId, av => av.MapFrom(src => src.UserId)));

        public static MapperConfiguration viewConfig = new MapperConfiguration(cgf => cgf.CreateMap<dm.Address, AddressViewModel>()
            .ForMember(a => a.AddressId, av => av.MapFrom(src => src.AddressId))
            .ForMember(a => a.Street, av => av.MapFrom(src => src.Street))
            .ForMember(a => a.City, av => av.MapFrom(src => src.City))
            .ForMember(a => a.State, av => av.MapFrom(src => src.State))
            .ForMember(a => a.PostalCode, av => av.MapFrom(src => src.PostalCode))
            .ForPath(a => a.UserId, av => av.MapFrom(src => src.User.UserId)));

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
        public AddressViewModel MappingAddressInfo(dm.Address address)
        {
            var addressViewMapper = viewConfig.CreateMapper();
            return addressViewMapper.Map<dm.Address, AddressViewModel> (address);
        }
    }
}
