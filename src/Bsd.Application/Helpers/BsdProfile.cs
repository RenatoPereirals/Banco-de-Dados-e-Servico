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
                .ForMember(dest => dest.DateService, opt => opt.MapFrom(src => src.DateServiceDate))
                .ReverseMap();

            CreateMap<MarkResponse, BsdEntity>()
                .ForMember(dest => dest.DateService, opt => opt.MapFrom<DateHelperValueResolver>())
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => new TimeOnly(src.Hora, src.Minuto)))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => new TimeOnly(src.Hora, src.Minuto)))
                .ForMember(dest => dest.DayType, opt => opt.Ignore())
                .ForMember(dest => dest.Employees, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<BsdEntity, List<ReportResponse>>()
                .ConvertUsing(src => src.Employees.SelectMany(assignment =>
                    assignment.Rubrics.Select(rubric => new ReportResponse
                    {
                        MatriculaPessoa = assignment.EmployeeId,
                        Rubric = rubric.RubricId,
                        TotalHours = rubric.TotalWorkedHours
                    })).ToList());



        }
    }
}