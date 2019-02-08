using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using SneakerDrop.Code.Helpers;
using dm = SneakerDrop.Domain.Models;

namespace SneakerDrop.Mvc.Models
{
    public class FindProductInfoViewModel
    {
        public int ProductInfoId { get; set; }

        public int BrandId { get; set; }

        public int TypeId { get; set; }

        [StringLength(50)]
        [Required]
        public string ProductTitle { get; set; }


        [StringLength(500)]
        [Required]
        public string Description { get; set; }

        [StringLength(50)]
        [Required]
        public string Color { get; set; }

        public void FindMatchingProductInfo(FindProductInfoViewModel findProduct)
        {
            var createModel = new ConvertingToDomainProductInfo();
            var validator = new dm.Validator();
            var productInfoDomainModel = createModel.MappingProductInfo(findProduct);
            var checkValidation = validator.ValidateProductTitle(productInfoDomainModel);

            if (checkValidation)
            {
                FindProductInfoHelper.FindPossibleMatches(productInfoDomainModel);
            }
        }
    }

    public class ConvertingToDomainProductInfo : Profile
    {
        public static MapperConfiguration productInfoConfig = new MapperConfiguration(cgf => cgf.CreateMap<FindProductInfoViewModel, dm.ProductInfo>()
            .ForMember(p => p.ProductInfoId, f => f.MapFrom(src => src.ProductInfoId))
            .ForMember(p => p.Brand.BrandId, f => f.MapFrom(src => src.BrandId))
            .ForMember(p => p.Type.TypeId, f => f.MapFrom(src => src.TypeId))
            .ForMember(p => p.ProductTitle, f => f.MapFrom(src => src.ProductTitle))
            .ForMember(p => p.Description, f => f.MapFrom(src => src.Description))
            .ForMember(p => p.Color, f => f.MapFrom(src => src.Color))
        );

        public dm.ProductInfo MappingProductInfo(FindProductInfoViewModel findProduct)
        {
            var productInfoMapper = productInfoConfig.CreateMapper();

            return productInfoMapper.Map<FindProductInfoViewModel, dm.ProductInfo>(findProduct);
        }
    }
}
