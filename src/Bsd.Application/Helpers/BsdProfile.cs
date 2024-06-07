using AutoMapper;
using Bsd.Application.DTOs;
using Bsd.Domain.Entities;

namespace Bsd.Application.Helpers
{
    public class BsdProfile : Profile
    {
        public BsdProfile()
        {
            CreateMap<CreateBsdRequest, BsdEntity>()
                .ForMember(dest => dest.DateService, opt => opt.MapFrom(src => src.DateServiceDate));
        }
    }
}