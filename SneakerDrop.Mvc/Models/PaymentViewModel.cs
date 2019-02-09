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


        public List<PaymentViewModel> PaymentValidator(PaymentViewModel paymentView)
        {
            var createModel = new ConversionPayment();
            var validator = new dm.Validator();
            dm.Payment paymentDomainModel = createModel.MappingPayment(paymentView);

            // validator info methods
            // if statement
            switch (paymentView.HelperType)
            {
                case "Add":
                    var valCheckAdd = validator.ValidateNewPayment(paymentDomainModel);

                    if (valCheckAdd)
                    {
                        PaymentHelper.AddPaymentById(paymentDomainModel);
                        return null;
                    }
                    // write error method call here
                    return null;
                case "Get":
                    List<dm.Payment> domainPaymentList = PaymentHelper.GetPaymentById(paymentDomainModel);
                    List<PaymentViewModel> viewPaymentList = createModel.MappingView(domainPaymentList);

                    return viewPaymentList;
                case "Delete":
                    PaymentHelper.DeletePaymentById(paymentDomainModel);
                    return null;
                default:
                    return null;
            }
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
            .ForMember(p => p.User.UserId, pv => pv.MapFrom(src => src.UserId)));

        public static MapperConfiguration viewConfig = new MapperConfiguration(cgf => cgf.CreateMap<dm.Payment, PaymentViewModel>()
            .ForMember(p => p.PaymentId, pv => pv.MapFrom(src => src.PaymentId))
            .ForMember(p => p.CCNumber, pv => pv.MapFrom(src => src.CCNumber))
            .ForMember(p => p.CCUserName, pv => pv.MapFrom(src => src.CCUserName))
            .ForMember(p => p.Month, pv => pv.MapFrom(src => src.Month))
            .ForMember(p => p.Year, pv => pv.MapFrom(src => src.Year))
            .ForMember(p => p.CVV, pv => pv.MapFrom(src => src.CVV))
            .ForMember(p => p.UserId, pv => pv.MapFrom(src => src.User.UserId)));

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
