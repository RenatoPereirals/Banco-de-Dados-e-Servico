using Bsd.Application.Interfaces;
using Bsd.Application.DTOs;

using Bsd.Domain.Services.Interfaces;
using Bsd.Domain.Entities;

using System.Text;

namespace Bsd.Application.Services;

public class ReportService : IReportService
{
    private readonly IMarkService _markService;
    private readonly IBsdApplicationService _bsdApplicationService;
    private readonly IStaticDataService _staticDataService;
    private readonly IDateHelper _dateHelper;

    public ReportService(IMarkService markService,
                         IBsdApplicationService bsdApplicationService,
                        IStaticDataService staticDataService,
                        IDateHelper dateHalper)
    {
        _markService = markService;
        _bsdApplicationService = bsdApplicationService;
        _staticDataService = staticDataService;
        _dateHelper = dateHalper;
    }

    public async Task<bool> ProcessReportByIdAsync(ReportRequest request, IEnumerable<Employee> employees)
    {
        var parseDate = _dateHelper.ParseDate(request.DataFim).AddDays(1);
        request.DataFim = _dateHelper.ParseString(parseDate);

        ICollection<MarkResponse> marks = await _markService.GetMarksForEmployeesAsync(employees, request)
            ?? throw new Exception("Não foi possível recuperar as marcações");

        ICollection<Employee> processedEmployees = _markService.ProcessMarksAsync(marks)
            ?? throw new Exception("Não foi possível processar as marcações");

        var bsdEntity = await _bsdApplicationService.CreatebsdEntityAsync(processedEmployees)
            ?? throw new Exception("Não foi possível criar as marcações");

        var outputPath = GenerateOutputPath()
            ?? throw new Exception("Local inválido para o relatatório.");

        bool reportGenerated = await CreateReportAsync(bsdEntity, outputPath);

        if (!reportGenerated)
            throw new Exception("Error generating report");

        return reportGenerated;
    }

    public async Task<bool> ProcessReportAsync(ReportRequest request)
    {
        var employees = _staticDataService.GetEmployees() 
            ?? throw new Exception("Employees not found");

        var parseDate = _dateHelper.ParseDate(request.DataFim).AddDays(1);
        request.DataFim = _dateHelper.ParseString(parseDate);

        ICollection<MarkResponse> marks = await _markService.GetMarksForEmployeesAsync(employees, request) 
            ?? throw new Exception("Não foi possível recuperar as marcações");

        ICollection<Employee> processedEmployees = _markService.ProcessMarksAsync(marks)
            ?? throw new Exception("Não foi possível processar as marcações");

        var bsdEntity = await _bsdApplicationService.CreatebsdEntityAsync(processedEmployees)
            ?? throw new Exception("Não foi possível criar as marcações");

        var outputPath = GenerateOutputPath() 
            ?? throw new Exception("Local inválido para o relatatório.");

        bool reportGenerated = await CreateReportAsync(bsdEntity, outputPath);

        if (!reportGenerated)
            throw new Exception("Error generating report");

        return reportGenerated;
    }

    private static string GenerateOutputPath()
    {
        var reportDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Report");

        if (!Directory.Exists(reportDirectory))
            Directory.CreateDirectory(reportDirectory);

        var fileName = $"Report_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
        return Path.Combine(reportDirectory, fileName);
    }


    private async Task<bool> CreateReportAsync(BsdEntity bsdEntity, string outputPath)
    {
        var reportResponses = new List<ReportResponse>();

        foreach (var employee in bsdEntity.Employees)
        {
            var employeeRubrics = employee.Rubrics
                .GroupBy(r => r.RubricId)
                .Select(g => new ReportResponse
                {
                    MatriculaPessoa = employee.EmployeeId,
                    Rubric = g.Key,
                    TotalHours = g.First().TotalWorkedHours
                })
                .ToList();

            reportResponses.AddRange(employeeRubrics);
        }

        reportResponses = reportResponses
            .OrderBy(r => r.MatriculaPessoa)
            .ThenBy(r => r.Rubric)
            .ToList();

        return await GenerateReportAsync(reportResponses, outputPath);
    }

    private async Task<bool> GenerateReportAsync(IEnumerable<ReportResponse> reportResponses, string outputPath)
    {
        if (reportResponses == null || !reportResponses.Any())
            return false;

        var lines = new List<string>();
        int? previousMatricula = null;

        foreach (var response in reportResponses)
        {
            lines.Add($"{response.MatriculaPessoa:D8};{response.Rubric:D5};{response.TotalHours:F2}");

            previousMatricula = response.MatriculaPessoa;
        }

        await File.WriteAllLinesAsync(outputPath, lines, Encoding.UTF8);

        return true;
    }
}
