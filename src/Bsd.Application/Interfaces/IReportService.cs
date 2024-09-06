using Bsd.Application.DTOs;

namespace Bsd.Application.Interfaces
{
    public interface IReportService
    {           
        Task<bool> ProcessReportAsync(ReportRequest request);
    }
}