using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using SneakerDrop.Code.Helpers;
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

        public int TotalQuantity { get; set; }

        public string ProductTitle { get; set; }

        public string Color { get; set; }

        public string ImageUrl { get; set; }

    }

    public class ConversionOrder : Profile
    {
        public static MapperConfiguration orderConfig = new MapperConfiguration(cgf => cgf.CreateMap<OrderAndPaymentViewModel, dm.Orders>()
            .ForMember(o => o.OrderId, op => op.MapFrom(src => src.OrderId))
            .ForMember(o => o.OrderGroupNumber, op => op.MapFrom(src => src.OrderGroupNumber))
            .ForMember(o => o.Quantity, op => op.MapFrom(src => src.Quantity))
            .ForMember(o => o.ShippingStatus, op => op.MapFrom(src => src.ShippingStatus))
            .ForMember(o => o.Timestamp, op => op.MapFrom(src => src.Timestamp))
            .ForPath(o => o.Payment.PaymentId, op => op.MapFrom(src => src.PaymentId))
            .ForPath(o => o.User.UserId, op => op.MapFrom(src => src.UserId))
            .ForPath(o => o.Listing.ListingId, op => op.MapFrom(src => src.ListingId)));

        public static MapperConfiguration viewConfig = new MapperConfiguration(cgf => cgf.CreateMap<dm.Orders, OrderAndPaymentViewModel>()
            .ForMember(o => o.OrderId, op => op.MapFrom(src => src.OrderId))
            .ForMember(o => o.OrderGroupNumber, op => op.MapFrom(src => src.OrderGroupNumber))
            .ForMember(o => o.Quantity, op => op.MapFrom(src => src.Quantity))
            .ForMember(o => o.ShippingStatus, op => op.MapFrom(src => src.ShippingStatus))
            .ForMember(o => o.Timestamp, op => op.MapFrom(src => src.Timestamp))
            .ForPath(o => o.PaymentId, op => op.MapFrom(src => src.Payment.PaymentId))
            .ForPath(o => o.UserId, op => op.MapFrom(src => src.User.UserId))
            .ForPath(o => o.ListingId, op => op.MapFrom(src => src.Listing.ListingId)));


        public dm.Orders MappingOrders(OrderAndPaymentViewModel orderView)
        {
            var orderMapper = orderConfig.CreateMapper();

            return orderMapper.Map<OrderAndPaymentViewModel, dm.Orders>(orderView);
        }

        public List<OrderAndPaymentViewModel> MappingView(List<dm.Orders> orders)
        {
            var orderModel = viewConfig.CreateMapper();
            List<OrderAndPaymentViewModel> convertedList = new List<OrderAndPaymentViewModel>();

            foreach (var item in orders)
            {
                var newItem = orderModel.Map<dm.Orders, OrderAndPaymentViewModel>(item);
                convertedList.Add(newItem);
            }
            return convertedList;
        }
    }
}
