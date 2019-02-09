using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using dm = SneakerDrop.Domain.Models;

namespace SneakerDrop.Mvc.Models
{
    public class OrderAndPaymentViewModel
    {
        public int OrderId { get; set; }

        public string HelperType { get; set; }

        [Required]
        public int OrderGroupNumber { get; set; }

        [Required]
        public int Quantity { get; set; }

        [StringLength(50)]
        [Required]
        public string ShippingStatus { get; set; }

        [Timestamp]
        public DateTime Timestamp { get; set; }

        [StringLength(50)]
        [Required]
        public string Size { get; set; }

        public int ListingId { get; set; }

        [Required]
        public decimal UserSetPrice { get; set; }

        public int PaymentId { get; set; }

        public int UserId { get; set; }

        public int ProductInfoId { get; set; }

        public List<OrderAndPaymentViewModel> GetOrders()
        {

        }
    }

    public class ConversionOrder : Profile
    {
        public static MapperConfiguration orderConfig = new MapperConfiguration(cgf => cgf.CreateMap<OrderAndPaymentViewModel, dm.Orders>()
            .ForMember(o => o.OrderId, op => op.MapFrom(src => src.OrderId))
            .ForMember(o => o.OrderGroupNumber, op => op.MapFrom(src => src.OrderGroupNumber))
            .ForMember(o => o.Quantity, op => op.MapFrom(src => src.Quantity))
            .ForMember(o => o.ShippingStatus, op => op.MapFrom(src => src.ShippingStatus))
            .ForMember(o => o.Timestamp, op => op.MapFrom(src => src.Timestamp))
            .ForMember(o => o.Payment.PaymentId, op => op.MapFrom(src => src.PaymentId))
            .ForMember(o => o.User.UserId, op => op.MapFrom(src => src.UserId)));

        public static MapperConfiguration listingConfig = new MapperConfiguration(cgf => cgf.CreateMap<OrderAndPaymentViewModel, dm.Listing>()
            .ForMember(l => l.ListingId, op => op.MapFrom(src => src.ListingId))
            .ForMember(l => l.UserSetPrice, op => op.MapFrom(src => src.UserSetPrice))
            .ForMember(l => l.Quantity, op => op.MapFrom(src => src.Quantity))
            .ForMember(l => l.Size, op => op.MapFrom(src => src.Size))
            .ForMember(l => l.User.UserId, op => op.MapFrom(src => src.UserId))
            .ForMember(l => l.ProductInfo.ProductInfoId, op => op.MapFrom(src => src.ProductInfoId))
            );
    }
}
