﻿using AutoMapper;
using Cerebri.API.DTOs;
using Cerebri.Domain.Entities;
namespace Cerebri.API.Mappings
{
    public class JournalEntryProfile : Profile
    {
        public static List<JournalEntryMoodModel> ConvertMoodsToJournalEntryMoods(List<MoodModel> moods)
        {
            return moods.Select(x => new JournalEntryMoodModel(x.Id)).ToList();
        }
        public JournalEntryProfile()
        {
            CreateMap<CreateJournalEntryDTO, JournalEntryModel>()
                .ForMember(dest => dest.Id, opt => opt.UseDestinationValue())
                .ForMember(dest => dest.UserId, opt => opt.UseDestinationValue())
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.MoodTags, opt => opt.MapFrom(src => ConvertMoodsToJournalEntryMoods(src.Moods)))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            CreateMap<UpdateJournalEntryDTO, JournalEntryModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.MoodTags, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            CreateMap<JournalEntryModel, JournalsResponseDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Moods, opt => opt.MapFrom(src => src.MoodTags))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));
        }
    }
}