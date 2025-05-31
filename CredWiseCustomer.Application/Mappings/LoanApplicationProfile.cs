using AutoMapper;
using CredWiseCustomer.Application.DTOs;
using CredWiseCustomer.Core.Entities;

namespace CredWiseCustomer.Application.Mappings;

public class LoanApplicationProfile : Profile
{
    public LoanApplicationProfile()
    {
        CreateMap<LoanApplication, LoanApplicationResponseDto>()
            .ForMember(dest => dest.LoanType, opt => opt.MapFrom(src => src.LoanProduct.LoanType))
            .ForMember(dest => dest.InterestRate, opt => opt.MapFrom(src => src.InterestRate))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));
    }
} 