using Bsd.Application.Interfaces;
using Bsd.Application.DTOs;

using System.Text;
using System.Security.Cryptography.X509Certificates;

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

        public string GenerateOutputPath()
        {
            var reportDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Report");

            if (!Directory.Exists(reportDirectory))
            {
                Directory.CreateDirectory(reportDirectory);
            }

            var fileName = $"Report_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

            var outputPath = Path.Combine(reportDirectory, fileName);

            return outputPath;
        }
    }
}
