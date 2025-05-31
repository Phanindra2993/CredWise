using AutoMapper;
using CredWiseCustomer.Application.DTOs;
using CredWiseCustomer.Core.Entities;
using System.Linq;

namespace CredWiseCustomer.Application.Mappings
{
    public class RepaymentProfile : Profile
    {
        public RepaymentProfile()
        {
            CreateMap<LoanRepaymentSchedule, RepaymentScheduleDto>()
                .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => src.PaymentTransactions.FirstOrDefault() != null ? src.PaymentTransactions.FirstOrDefault().PaymentMethod : null));
        }
    }
} 