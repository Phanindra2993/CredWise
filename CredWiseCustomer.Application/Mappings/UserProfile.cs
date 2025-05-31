//using AutoMapper;
//using CredWiseCustomer.Application.DTOs;
//using CredWiseCustomer.Core.Entities;

//namespace CredWiseCustomer.Application.Mappings
//{
//    public class UserProfile : Profile
//    {
//        public UserProfile()
//        {
//            CreateMap<User, UserDto>()
//                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
//                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
//                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
//                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
//                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
//                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
//                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
//                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy));

//            CreateMap<CreateUserDto, User>()
//                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
//                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
//                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
//                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
//                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
//                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "Customer")) // Default role
//                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true)) // Default to active
//                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
//                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy));

//            CreateMap<UpdateUserDto, User>()
//                .ForMember(dest => dest.ModifiedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
//                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
//        }
//    }
//}


using AutoMapper;
using CredWiseCustomer.Application.DTOs;
using CredWiseCustomer.Core.Entities;

namespace CredWiseCustomer.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy));

            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src =>
                    src.CreatedBy.Equals("Admin", StringComparison.OrdinalIgnoreCase)
                        ? "Admin"
                        : "Customer")) // Set role based on CreatedBy
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy));

            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.ModifiedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                //.ForMember(dest => dest.Role, opt => opt.MapFrom(src =>
                //    !string.IsNullOrEmpty(src.Role)
                //        ? src.Role.Equals("admin", StringComparison.OrdinalIgnoreCase)
                //            ? "Admin"
                //            : "Customer"
                //        : null)) // Preserve existing role if not specified
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
