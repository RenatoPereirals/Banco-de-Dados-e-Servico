using Bsd.Application.DTOs;
using Bsd.Domain.Entities;

namespace Bsd.Application.Interfaces
{
    public interface IReportService
    {           
        Task<bool> ProcessReportAsync(ReportRequest request);
        Task<bool> ProcessReportByIdAsync(ReportRequest request, IEnumerable<Employee> employees);
    }
}