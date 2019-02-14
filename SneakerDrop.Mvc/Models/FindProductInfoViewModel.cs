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

        public string HelperType { get; set; }

        public int BrandId { get; set; }

        public int TypeId { get; set; }

        [StringLength(50)]
        [Required]
        public string ProductTitle { get; set; }

        public decimal DisplayPrice { get; set; }


        [StringLength(500)]
        [Required]
        public string Description { get; set; }

        [StringLength(50)]
        [Required]
        public string Color { get; set; }

        public string ImageUrl { get; set; }

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

        public List<FindProductInfoViewModel> ConvertListOnly(List<dm.ProductInfo> list)
        {
            return createModel.MappingViewInfo(list);

        }

        public dm.ProductInfo SelectedViewModel(FindProductInfoViewModel item)
        {
            var productDomainModel = createModel.MappingProductInfo(item);
            return FindProductInfoHelper.SingleProductInfo(productDomainModel);
        }

        public List<FindProductInfoViewModel> FindMostRecentListings()
        {
            var domainList = FindProductInfoHelper.GetAllRecentProducts();
            List<FindProductInfoViewModel> convertedList = createModel.MappingRecentListing(domainList);

            return convertedList;
        }

        public List<FindProductInfoViewModel> SearchFind(string Search)
        {
            var searchlist = FindProductInfoHelper.FindSearch(Search);
            List<FindProductInfoViewModel> searchconverted = createModel.MappingRecentListing(searchlist);
            return searchconverted;
        }
    }

    public class ConversionProduct : Profile
    {
        public static MapperConfiguration productInfoConfig = new MapperConfiguration(cgf => cgf.CreateMap<FindProductInfoViewModel, dm.ProductInfo>()
            .ForMember(p => p.ProductInfoId, f => f.MapFrom(src => src.ProductInfoId))
            .ForPath(p => p.Brand.BrandId, f => f.MapFrom(src => src.BrandId))
            .ForPath(p => p.Type.TypeId, f => f.MapFrom(src => src.TypeId))
            .ForMember(p => p.ProductTitle, f => f.MapFrom(src => src.ProductTitle))
            .ForMember(p => p.Description, f => f.MapFrom(src => src.Description))
            .ForMember(p => p.Color, f => f.MapFrom(src => src.Color))
            .ForMember(p => p.ImageUrl, f => f.MapFrom(src => src.ImageUrl))
             .ForMember(p => p.DisplayPrice, f => f.MapFrom(src => src.DisplayPrice)));


        public static MapperConfiguration viewConfig = new MapperConfiguration(cgf => cgf.CreateMap<dm.ProductInfo, FindProductInfoViewModel>()
            .ForMember(p => p.ProductInfoId, f => f.MapFrom(src => src.ProductInfoId))
            .ForPath(p => p.BrandId, f => f.MapFrom(src => src.Brand.BrandId))
            .ForPath(p => p.TypeId, f => f.MapFrom(src => src.Type.TypeId))
            .ForMember(p => p.ProductTitle, f => f.MapFrom(src => src.ProductTitle))
            .ForMember(p => p.Description, f => f.MapFrom(src => src.Description))
            .ForMember(p => p.Color, f => f.MapFrom(src => src.Color))
            .ForMember(p => p.ImageUrl, f => f.MapFrom(src => src.ImageUrl))
             .ForMember(p => p.DisplayPrice, f => f.MapFrom(src => src.DisplayPrice)));

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

        public List<FindProductInfoViewModel> MappingRecentListing(IEnumerable<dm.ProductInfo> domainList)
        {
            var viewMapper = viewConfig.CreateMapper();
            List<FindProductInfoViewModel> convertedList = new List<FindProductInfoViewModel>();

            foreach (var item in domainList)
            {
                var newItem = viewMapper.Map<dm.ProductInfo, FindProductInfoViewModel>(item);
                convertedList.Add(newItem);
            }

            return convertedList;
        }
       



    }

}
