﻿using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using SneakerDrop.Code.Helpers;
using dm = SneakerDrop.Domain.Models;

namespace SneakerDrop.Mvc.Models
{
    public class CreateNewListingViewModel
    {
        public int ListingId { get; set; }

        [Required]
        public decimal UserSetPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        [StringLength(50)]
        [Required]
        public string Size { get; set; }

        public int UserId { get; set; }

        public int ProductInfoId { get; set; }

        public int BrandId { get; set; }

        [StringLength(50)]
        [Required]
        public string BrandName { get; set; }

        public int TypeId { get; set; }

        [StringLength(50)]
        [Required]
        public string TypeName { get; set; }

        [StringLength(50)]
        [Required]
        public string ProductTitle { get; set; }

        [StringLength(500)]
        [Required]
        public string Description { get; set; }

        [Required]
        public int DisplayPrice { get; set; }

        [StringLength(50)]
        [Required]
        public string ReleaseDate { get; set; }

        [StringLength(50)]
        [Required]
        public string Color { get; set; }

        public string ImageUrl {get; set; }

        public ConversionNewListing createModel = new ConversionNewListing();

        public dm.Validator validator = new dm.Validator();

        public CreateNewListingViewModel CreateNewListing(dm.ProductInfo specificProduct)
        {
            var productInfo = FindProductInfoHelper.SingleProductInfo(specificProduct);

            return createModel.MappingCreateListing(productInfo);
        }

        public bool AddListingToDb(CreateNewListingViewModel listing)
        {
            var newItem = createModel.MappingDomainListing(listing);
            ListingHelper.AddListingById(newItem);

            return true;
        }
    }

    public class ConversionNewListing : Profile
    {
        public static MapperConfiguration listingConfig = new MapperConfiguration(cgf => cgf.CreateMap<dm.ProductInfo, CreateNewListingViewModel>()
            .ForPath(c => c.BrandId, pf => pf.MapFrom(src => src.Brand.BrandId))
            .ForPath(c => c.BrandName, pf => pf.MapFrom(src => src.Brand.BrandName))
            .ForPath(c => c.TypeId, pf => pf.MapFrom(src => src.Type.TypeId))
            .ForPath(c => c.TypeName, pf => pf.MapFrom(src => src.Type.TypeName))
            .ForMember(c => c.ProductTitle, pf => pf.MapFrom(src => src.ProductTitle))
            .ForMember(c => c.Color, pf => pf.MapFrom(src => src.Color))
            .ForMember(c => c.ImageUrl, pf => pf.MapFrom(src => src.ImageUrl))
            .ForAllOtherMembers(c => c.Ignore()));

        public static MapperConfiguration domainConfig = new MapperConfiguration(cgf => cgf.CreateMap<CreateNewListingViewModel, dm.Listing>()
            .ForMember(l => l.Quantity, nl => nl.MapFrom(src => src.Quantity))
            .ForMember(l => l.UserSetPrice, nl => nl.MapFrom(src => src.UserSetPrice))
            .ForMember(l => l.Size, nl => nl.MapFrom(src => src.Size))
            .ForMember(l => l.User.UserId, nl => nl.MapFrom(src => src.UserId))
            .ForMember(l => l.ProductInfo.ProductInfoId, nl => nl.MapFrom(src => src.ProductInfoId))
            .ForMember(l => l.ProductInfo.ProductTitle, nl => nl.MapFrom(src => src.ProductTitle))
            .ForMember(l => l.ProductInfo.ImageUrl, nl => nl.MapFrom(src => src.ImageUrl))
            .ForAllOtherMembers(c => c.Ignore()));

        public CreateNewListingViewModel MappingCreateListing(dm.ProductInfo item)
        {
            var listingMapper = listingConfig.CreateMapper();
            return listingMapper.Map<dm.ProductInfo, CreateNewListingViewModel>(item);
        }

        public dm.Listing MappingDomainListing(CreateNewListingViewModel listing)
        {
            var listingMapper = domainConfig.CreateMapper();
            return listingMapper.Map<CreateNewListingViewModel, dm.Listing>(listing);
        }
    }
}
