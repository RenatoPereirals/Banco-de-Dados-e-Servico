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
        private readonly IBsdService _bsdService;

        public BsdApplicationService(IExternalApiService externalApiService,
                                     IMarkService markService,
                                     IReportService reportService,
                                     IStaticDataService staticDataService,
                                     IBsdService bsdService)
        {
            _externalApiService = externalApiService;
            _markService = markService;
            _reportService = reportService;
            _staticDataService = staticDataService;
            _bsdService = bsdService;
        }

        public async Task<bool> GenerateReportAsync(ReportRequest request)
        {
            var employees = await GetEmployeesAsync();
            var marks = await GetMarksForEmployeesAsync(employees, request);

            // Processa as marcas para gerar os WorkedDays e associá-los aos funcionários
            // esse método não está associando corretamente as datas de inicio e fim dos dias trabalhados e as horas de entrada e saída
            var processedEmployees = await _markService.ProcessMarksAsync(marks);
            // Cria os bsdEntity com os funcionários processados
            var bsdEntity = await CreatebsdEntity(processedEmployees);

            var outputPath = _reportService.GenerateOutputPath();
            var reportGenerated = await CreateReportAsync(bsdEntity, outputPath);

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

        // Método para criar os bsdEntity com os funcionários processados
        private async Task<BsdEntity> CreatebsdEntity(BsdEntity bsdEntity)
        {
            await _bsdService.CreateOrUpdateBsdsAsync(bsdEntity);

            return bsdEntity;
        }

        private async Task<bool> CreateReportAsync(BsdEntity bsdEntity, string outputPath)
        {
            var reportResponses = bsdEntity
                .Employees
                .SelectMany(employee => employee.Rubrics, (employee, rubric) => new ReportResponse
                {
                    MatriculaPessoa = employee.EmployeeId,
                    Rubric = rubric.RubricId,
                    TotalHours = rubric.TotalWorkedHours
                })
                .GroupBy(x => new { x.MatriculaPessoa, x.Rubric })
                .Select(g => new ReportResponse
                {
                    MatriculaPessoa = g.Key.MatriculaPessoa,
                    Rubric = g.Key.Rubric,
                    TotalHours = g.Sum(x => x.TotalHours)
                })
                .OrderBy(r => r.MatriculaPessoa)
                .ThenBy(r => r.Rubric)
                .ToList();

            return await _reportService.GenerateReport(reportResponses, outputPath);
        }
    }
}