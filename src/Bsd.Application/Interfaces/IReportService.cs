using Bsd.Application.DTOs;

namespace Bsd.Application.Interfaces
{
    public interface IReportService
    {
        Task<bool> GenerateReport(IEnumerable<ReportResponse> reportResponses, string outputPath);
    }
}