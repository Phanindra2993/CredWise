using AutoMapper;
using CredWiseCustomer.Application.DTOs;
using CredWiseCustomer.Core.Entities;

namespace CredWiseCustomer.Application.Mappings
{
    public class LoanEnquiryProfile : Profile
    {
        public LoanEnquiryProfile()
        {
            CreateMap<LoanEnquiryRequestDto, LoanEnquiry>()
                .ForMember(dest => dest.LoanAmountRequired, opt => opt.MapFrom(src => src.LoanAmount))
                .ForMember(dest => dest.LoanPurpose, opt => opt.MapFrom(src => src.LoanPurpose))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));
            CreateMap<LoanEnquiry, LoanEnquiryResponseDto>();
        }
    }
} 