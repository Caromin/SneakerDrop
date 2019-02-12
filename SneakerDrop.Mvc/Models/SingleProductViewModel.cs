using System;
using System.Collections.Generic;
using AutoMapper;
using SneakerDrop.Code.Helpers;
using dm = SneakerDrop.Domain.Models;

namespace SneakerDrop.Mvc.Models
{
    public class SingleProductViewModel
    {
        public int ProductInfoId { get; set; }

        public int BrandId { get; set; }

        public string BrandName { get; set; }

        public int TypeId { get; set; }

        public string TypeName { get; set; }

        public string ProductTitle { get; set; }

        public string Description { get; set; }

        public int DisplayPrice { get; set; }

        public string ReleaseDate { get; set; }

        public string Color { get; set; }

        public int ListingId { get; set; }

        public decimal UserSetPrice { get; set; }

        public int Quantity { get; set; }

        public string Size { get; set; }

        public int SellerId { get; set; }

        public string ImageUrl { get; set; }

        public ConversionListing createModel = new ConversionListing();

        public List<SingleProductViewModel> FindAllMatchingListings(FindProductInfoViewModel productView)
        {
            var domainModel = createModel.MappingListing(productView);
            var domainList = ListingHelper.GetAllListingsByProductName(domainModel);

            return createModel.MappingAllViewListings(domainList);
        }
    }

    public class ConversionListing : Profile
    {
        public static MapperConfiguration listingConfig = new MapperConfiguration(cgf => cgf.CreateMap<FindProductInfoViewModel, dm.Listing>()
            .ForPath(p => p.ProductInfo.ProductTitle, f => f.MapFrom(src => src.ProductTitle))
            .ForPath(p => p.ProductInfo.Description, f => f.MapFrom(src => src.Description))
            .ForPath(p => p.ProductInfo.Color, f => f.MapFrom(src => src.Color))
            .ForAllOtherMembers(c => c.Ignore()));

        public static MapperConfiguration viewConfig = new MapperConfiguration(cgf => cgf.CreateMap<dm.Listing, SingleProductViewModel>()
            .ForMember(p => p.ListingId, f => f.MapFrom(src => src.ListingId))
            .ForMember(p => p.UserSetPrice, f => f.MapFrom(src => src.UserSetPrice))
            .ForMember(p => p.Quantity, f => f.MapFrom(src => src.Quantity))
            .ForMember(p => p.Size, f => f.MapFrom(src => src.Size))
            .ForMember(p => p.SellerId, f => f.MapFrom(src => src.User.UserId))
            .ForAllOtherMembers(c => c.Ignore()));

        public dm.Listing MappingListing(FindProductInfoViewModel item)
        {
            var listingMapper = listingConfig.CreateMapper();

            return listingMapper.Map<FindProductInfoViewModel, dm.Listing>(item);
        }

        public List<SingleProductViewModel> MappingAllViewListings(List<dm.Listing> listings)
        {
            var viewMapper = viewConfig.CreateMapper();
            List<SingleProductViewModel> convertedList = new List<SingleProductViewModel>();

            foreach (var item in listings)
            {
                var newItem = viewMapper.Map<dm.Listing, SingleProductViewModel>(item);
                convertedList.Add(newItem);
            }
            return convertedList;
        }

      
    }
}
