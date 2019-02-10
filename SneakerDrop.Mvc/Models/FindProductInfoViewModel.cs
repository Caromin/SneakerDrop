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

        public ConversionProduct createModel = new ConversionProduct();

        public dm.Validator validator = new dm.Validator();

        public List<FindProductInfoViewModel> FindMatchingProductInfo(FindProductInfoViewModel findProduct)
        {
            var productInfoDomainModel = createModel.MappingProductInfo(findProduct);
            var checkValidation = validator.ValidateProductTitle(productInfoDomainModel);

            if (checkValidation)
            {
                List<dm.ProductInfo> results = FindProductInfoHelper.FindPossibleMatches(productInfoDomainModel);
                List<FindProductInfoViewModel> productViewModel = createModel.MappingViewInfo(results);

                return productViewModel;
            }
            return null;
        }

        //public FindProductInfoViewModel SelectedViewModel(FindProductInfoViewModel item)
        //{
        //    var productDomainModel = createModel.
        //    return FindProductInfoHelper.SingleProductInfo(item);
        //}
    }

    public class ConversionProduct : Profile
    {
        public static MapperConfiguration productInfoConfig = new MapperConfiguration(cgf => cgf.CreateMap<FindProductInfoViewModel, dm.ProductInfo>()
            .ForMember(p => p.ProductInfoId, f => f.MapFrom(src => src.ProductInfoId))
            .ForMember(p => p.Brand.BrandId, f => f.MapFrom(src => src.BrandId))
            .ForMember(p => p.Type.TypeId, f => f.MapFrom(src => src.TypeId))
            .ForMember(p => p.ProductTitle, f => f.MapFrom(src => src.ProductTitle))
            .ForMember(p => p.Description, f => f.MapFrom(src => src.Description))
            .ForMember(p => p.Color, f => f.MapFrom(src => src.Color))
        );

        public static MapperConfiguration viewConfig = new MapperConfiguration(cgf => cgf.CreateMap<dm.ProductInfo, FindProductInfoViewModel>()
            .ForMember(p => p.ProductInfoId, f => f.MapFrom(src => src.ProductInfoId))
            .ForMember(p => p.BrandId, f => f.MapFrom(src => src.Brand.BrandId))
            .ForMember(p => p.TypeId, f => f.MapFrom(src => src.Type.TypeId))
            .ForMember(p => p.ProductTitle, f => f.MapFrom(src => src.ProductTitle))
            .ForMember(p => p.Description, f => f.MapFrom(src => src.Description))
            .ForMember(p => p.Color, f => f.MapFrom(src => src.Color)));

        public dm.ProductInfo MappingProductInfo(FindProductInfoViewModel findProduct)
        {
            var productInfoMapper = productInfoConfig.CreateMapper();

            return productInfoMapper.Map<FindProductInfoViewModel, dm.ProductInfo>(findProduct);
        }

        public List<FindProductInfoViewModel> MappingViewInfo(List<dm.ProductInfo> product)
        {
            var productInfo = viewConfig.CreateMapper();

            List<FindProductInfoViewModel> convertedList = new List<FindProductInfoViewModel>();

            foreach (var item in product)
            {
                var newItem = productInfo.Map<dm.ProductInfo, FindProductInfoViewModel>(item);
                convertedList.Add(newItem);
            }

            return convertedList;
        }
    }

}
