using AutoMapper;
using Cerebri.API.DTOs;
using Cerebri.Domain.Entities;
namespace Cerebri.API.Mappings
{
    public class JournalEntryProfile : Profile
    {
        public JournalEntryProfile()
        {
            CreateMap<CreateJournalEntryDTO, JournalEntryModel>()
                .ForMember(dest => dest.Id, opt => opt.UseDestinationValue())
                .ForMember(dest => dest.UserId, opt => opt.UseDestinationValue())
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.MoodTags, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
        }
    }
}