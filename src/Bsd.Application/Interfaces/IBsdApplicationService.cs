using Bsd.Application.DTOs;

namespace Bsd.Application.Interfaces
{
    public interface IBsdApplicationService
    {
        Task<bool> GenerateReportAsync(ReportRequest request);
    }
}