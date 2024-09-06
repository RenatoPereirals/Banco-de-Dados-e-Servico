using Bsd.Application.DTOs;

using Bsd.Domain.Entities;

namespace Bsd.Application.Interfaces;

public interface IMarkService
{
    Task<ICollection<MarkResponse>> GetMarksForEmployeesAsync(IEnumerable<Employee> employees, ReportRequest request);
    ICollection<Employee> ProcessMarksAsync(ICollection<MarkResponse> markResponses);
}