using Bsd.Application.Interfaces;
using Bsd.Application.DTOs;

using Bsd.Domain.Services.Interfaces;
using Bsd.Domain.Entities;


namespace Bsd.Application.Services
{
    public class BsdApplicationService : IBsdApplicationService
    {
        private readonly IExternalApiService _externalApiService;
        private readonly IMarkService _markService;
        private readonly IReportService _reportService;
        private readonly IStaticDataService _staticDataService;

        public BsdApplicationService(IExternalApiService externalApiService,
                                     IMarkService markService,
                                     IReportService reportService,
                                     IStaticDataService staticDataService)
        {
            _externalApiService = externalApiService;
            _markService = markService;
            _reportService = reportService;
            _staticDataService = staticDataService;
        }

        public async Task<bool> GenerateReportAsync(ReportRequest request)
        {
            var employees = await GetEmployeesAsync();
            var marks = await GetMarksForEmployeesAsync(employees, request);

            var processedMarks = await _markService.ProcessMarksAsync(marks);

            var reportGenerated = await GenerateReportAsync(processedMarks, request.OutputPath);

            return reportGenerated;
        }

        private async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await _staticDataService.GetEmployeesAsync();
        }

        private async Task<ICollection<MarkResponse>> GetMarksForEmployeesAsync(IEnumerable<Employee> employees, ReportRequest request)
        {
            var employeeIds = employees.Select(e => e.EmployeeId).ToList();
            var markRequest = new MarkRequest(employeeIds)
            {
                DataInicio = request.DataInicio,
                DataFim = request.DataFim
            };
            return await _externalApiService.GetMarkAsync(markRequest);
        }

        private async Task<bool> GenerateReportAsync(ICollection<BsdEntity> processedMarks, string outputPath)
        {
            var reportResponses = processedMarks
                .SelectMany(bsdEntity => bsdEntity.Employees
                    .SelectMany(employee => employee.Rubrics.Select(rubric => new
                    {
                        employee.EmployeeId,
                        rubric.RubricId,
                        TotalHours = rubric.TotalWorkedHours
                    })))
                .GroupBy(x => new { x.EmployeeId, x.RubricId })
                .Select(g => new ReportResponse
                {
                    MatriculaPessoa = g.Key.EmployeeId,
                    Rubric = g.Key.RubricId,
                    TotalHours = g.Sum(x => x.TotalHours)
                })
                .OrderBy(r => r.MatriculaPessoa)
                .ThenBy(r => r.Rubric)
                .ToList();

            return await _reportService.GenerateReport(reportResponses, outputPath);
        }

    }
}