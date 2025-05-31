using AutoMapper;
using CredWiseCustomer.Application.DTOs;
using CredWiseCustomer.Core.Entities;

namespace CredWiseCustomer.Application.Mappings
{
    public class FdProfile : Profile
    {
        public FdProfile()
        {
            CreateMap<ApplyFdDto, Fdapplication>();
            CreateMap<Fdapplication, FdStatusDto>()
                .ForMember(dest => dest.FdTypeName, opt => opt.MapFrom(src => src.Fdtype != null ? src.Fdtype.Name : null));
        }
    }
}