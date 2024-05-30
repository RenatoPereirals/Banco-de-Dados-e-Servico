using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Bsd.Application.DTOs;
using Bsd.Domain.Entities;

namespace Bsd.Application.Helpers
{
    public class BsdProfile : Profile
    {
        public BsdProfile()
        {
            CreateMap<BsdEntity, BsdEntityDto>().ReverseMap();
            CreateMap<CreateBsdRequest, BsdEntityDto>().ReverseMap();
        }
    }
}