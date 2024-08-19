using Bsd.Application.Interfaces;
using Bsd.Application.DTOs;

using System.Text;

namespace Bsd.Application.Services
{
    public class ReportService : IReportService
    {
        public async Task<bool> GenerateReport(IEnumerable<ReportResponse> reportResponses, string outputPath)
        {
            if (reportResponses == null || !reportResponses.Any())
                return false;

            var lines = reportResponses.Select(r => $"{r.MatriculaPessoa};{r.Rubric};{r.TotalHours}");

            await File.WriteAllLinesAsync(outputPath, lines, Encoding.UTF8);

            return true;
        }
    }
}
