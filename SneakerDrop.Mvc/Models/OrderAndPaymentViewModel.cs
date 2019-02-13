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

        public ConversionOrder createModel = new ConversionOrder();

        public dm.Validator validator = new dm.Validator();

        public List<OrderAndPaymentViewModel> GetOrderHistory(OrderAndPaymentViewModel orderView)
        {
            dm.Orders orderDomainModel = createModel.MappingOrders(orderView);
            List<dm.Orders> orderDomainList = OrderHelper.GetOrdersById(orderDomainModel);

            return createModel.MappingView(orderDomainList);
        }

        // add order needs userId, cancel order needs orderId, no validation setup
        public bool AddOrCancelOrders(OrderAndPaymentViewModel orderView)
        {
            dm.Orders orderDomainModel = createModel.MappingOrders(orderView); 

            dm.User getUser = UserHelper.GetUserInfoByIdForOrder(orderDomainModel);
            dm.Listing getListing = ListingHelper.GetListingInfoByIdForOrder(orderDomainModel);
            dm.User getUserListing = UserHelper.GetUserInfobyIdForListing(getListing);
            dm.Payment getPayment = PaymentHelper.GetPaymentByOrderId(orderDomainModel);

            if (orderView.HelperType == "add")
            {
                var addedOrder = new dm.Orders
                {
                    OrderId = orderDomainModel.OrderId,
                    OrderGroupNumber = orderDomainModel.OrderGroupNumber,
                    Quantity = orderDomainModel.Quantity,
                    ShippingStatus = orderDomainModel.ShippingStatus,
                    Timestamp = orderDomainModel.Timestamp,

                    User = new dm.User
                    {
                        UserId = getUser.UserId,
                        Username = getUser.Username,
                        Password = getUser.Password,
                        Firstname = getUser.Firstname,
                        Lastname = getUser.Lastname,
                        Email = getUser.Email,
                    },
                    Listing = new dm.Listing
                    {
                        ListingId = getListing.ListingId,
                        Quantity = getListing.Quantity,
                        Size = getListing.Size,
                        UserSetPrice = getListing.UserSetPrice,
                        ProductInfo = getListing.ProductInfo,
                        User = getListing.User,                        
                    },
                    Payment = new dm.Payment
                    {
                        PaymentId = getPayment.PaymentId,
                        CCNumber = getPayment.CCNumber,
                        CCUserName = getPayment.CCUserName,
                        Month = getPayment.Month,
                        Year = getPayment.Year,
                        CVV = getPayment.CVV,
                        User = getPayment.User,
                        
                    },
                };
                OrderHelper.AddOrderById(addedOrder);
                return true;
            }
            OrderHelper.CancelOrderByOrderId(orderDomainModel);
            return true;
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
