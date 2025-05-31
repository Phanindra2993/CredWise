using AutoMapper;
using CredWiseCustomer.Application.DTOs;
using CredWiseCustomer.Core.Entities;

namespace CredWiseCustomer.Application.Mappings
{
    public class LoanProfile : Profile
    {
        public LoanProfile()
        {
            CreateMap<ApplyLoanDto, LoanApplication>()
                .ForMember(dest => dest.Dob, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.DOB)))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy));
            CreateMap<LoanApplication, LoanStatusDto>();
        }
    }
}