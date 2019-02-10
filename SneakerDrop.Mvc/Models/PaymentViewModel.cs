using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using SneakerDrop.Code.Helpers;
using dm = SneakerDrop.Domain.Models;

namespace SneakerDrop.Mvc.Models
{
    public class PaymentViewModel
    {
        public string HelperType { get; set; }

        public int PaymentId { get; set; }

        [Required]
        public long CCNumber { get; set; }

        [StringLength(50)]
        [Required]
        public string CCUserName { get; set; }

        [Required]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int CVV { get; set; }

        public int UserId { get; set; }

        public ConversionPayment createModel = new ConversionPayment();

        public dm.Validator validator = new dm.Validator();

        // In the user homepage when payment is selected, userId is passed in PaymentViewModel format
        public List<PaymentViewModel> GetAllPayments(PaymentViewModel paymentView)
        {
            dm.Payment paymentDomainModel = createModel.MappingPayment(paymentView);
            List<dm.Payment> domainPaymentList = PaymentHelper.GetPaymentById(paymentDomainModel);

            return createModel.MappingView(domainPaymentList);
        }

        // add requires all properties, delete only needs
        public bool AddOrDeletePayments(PaymentViewModel paymentView)
        {
            dm.Payment paymentDomainModel = createModel.MappingPayment(paymentView);
            var valCheckAdd = validator.ValidateNewPayment(paymentDomainModel);

            if (paymentView.HelperType == "add")
            {
                if (valCheckAdd)
                {
                    PaymentHelper.AddPaymentById(paymentDomainModel);
                    return true;
                }
                return false;
            }
            PaymentHelper.DeletePaymentByPaymentId(paymentDomainModel);
            return true;
        }
    }

    public class ConversionPayment : Profile
    {
        public static MapperConfiguration paymentConfig = new MapperConfiguration(cgf => cgf.CreateMap<PaymentViewModel, dm.Payment>()
            .ForMember(p => p.PaymentId, pv => pv.MapFrom(src => src.PaymentId))
            .ForMember(p => p.CCNumber, pv => pv.MapFrom(src => src.CCNumber))
            .ForMember(p => p.CCUserName, pv => pv.MapFrom(src => src.CCUserName))
            .ForMember(p => p.Month, pv => pv.MapFrom(src => src.Month))
            .ForMember(p => p.Year, pv => pv.MapFrom(src => src.Year))
            .ForMember(p => p.CVV, pv => pv.MapFrom(src => src.CVV))
            .ForPath(p => p.User.UserId, pv => pv.MapFrom(src => src.UserId)));

        public static MapperConfiguration viewConfig = new MapperConfiguration(cgf => cgf.CreateMap<dm.Payment, PaymentViewModel>()
            .ForMember(p => p.PaymentId, pv => pv.MapFrom(src => src.PaymentId))
            .ForMember(p => p.CCNumber, pv => pv.MapFrom(src => src.CCNumber))
            .ForMember(p => p.CCUserName, pv => pv.MapFrom(src => src.CCUserName))
            .ForMember(p => p.Month, pv => pv.MapFrom(src => src.Month))
            .ForMember(p => p.Year, pv => pv.MapFrom(src => src.Year))
            .ForMember(p => p.CVV, pv => pv.MapFrom(src => src.CVV))
            .ForPath(p => p.UserId, pv => pv.MapFrom(src => src.User.UserId)));

        public dm.Payment MappingPayment(PaymentViewModel paymentView)
        {
            var paymentMapper = paymentConfig.CreateMapper();

            return paymentMapper.Map<PaymentViewModel, dm.Payment>(paymentView);
        }

        public List<PaymentViewModel> MappingView(List<dm.Payment> payment)
        {
            var paymentModel = viewConfig.CreateMapper();
            List<PaymentViewModel> convertedList = new List<PaymentViewModel>();

            foreach (var item in payment)
            {
                var newItem = paymentModel.Map<dm.Payment, PaymentViewModel>(item);
                convertedList.Add(newItem);
            }

            return convertedList;
        }


    }
}
