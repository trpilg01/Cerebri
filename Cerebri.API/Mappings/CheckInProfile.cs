using AutoMapper;
using Cerebri.API.DTOs;
using Cerebri.Domain.Entities;

namespace Cerebri.API.Mappings
{
    public class CheckInProfile : Profile
    {
        public static List<CheckInMoodModel> ConvertMoodsToCheckInMoodModels(List<MoodModel> moods)
        {
            return moods?.ConvertAll(x => new CheckInMoodModel(x.Id)) ?? new List<CheckInMoodModel>();
        }

        public CheckInProfile() 
        {
            CreateMap<CreateCheckInDTO, CheckInModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.MoodTags, opt => opt.MapFrom(src => ConvertMoodsToCheckInMoodModels(src.Moods)))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            CreateMap<CheckInModel, ResponseCheckInDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.Moods, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));
        }
    }
}
