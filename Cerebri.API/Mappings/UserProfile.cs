using AutoMapper;
using Cerebri.API.DTOs;
using Cerebri.Domain.Entities;

namespace Cerebri.API.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDTO, UserModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.HashedPassword, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.JournalEntries, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            CreateMap<UserModel, UserInfoDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName));

            CreateMap<UpdateUserRequestDTO, UserModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.HashedPassword, opt => opt.Ignore())
                .ForMember(dest => dest.Reports, opt => opt.Ignore())
                .ForMember(dest => dest.JournalEntries, opt => opt.Ignore())
                .ForMember(dest => dest.CheckIns, opt => opt.Ignore());
        }
    }
}
