using AutoMapper;
using Cerebri.API.DTOs;
using Cerebri.Domain.Entities;

namespace Cerebri.API.Mappings
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<ReportModel, ReportResponseDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ReportName, opt => opt.MapFrom(src => src.ReportName))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));
        }
    }
}
