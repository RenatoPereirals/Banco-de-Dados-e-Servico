using AutoMapper;

using Bsd.Application.DTOs;
using Bsd.Domain.Entities;

namespace Bsd.Application.Helpers
{
    public class BsdProfile : Profile
    {
        public BsdProfile()
        {
            CreateMap<MarkResponse, BsdEntity>()
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