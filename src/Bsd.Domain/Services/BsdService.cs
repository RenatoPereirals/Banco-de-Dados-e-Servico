using Bsd.Domain.Services.Interfaces;
using Bsd.Domain.Entities;

namespace Bsd.Domain.Services;

public class BsdService : IBsdService
{
    private readonly ICalculateRubricHours _calculateRubricHours;
    private readonly IRubricService _rubricService;

    public BsdService(ICalculateRubricHours calculateRubricHours, IRubricService rubricService)
    {
        _calculateRubricHours = calculateRubricHours;
        _rubricService = rubricService;
    }

    public async Task CreateOrUpdateBsdsAsync(BsdEntity bsd)
    {
        await _rubricService.AssociateRubricsToEmployeeAsync(bsd);

        foreach (var employee in bsd.Employees)
        {
            var duplicatedRubrics = employee.Rubrics
                .GroupBy(r => r.RubricId)
                .Where(g => g.Count() > 1)
                .ToList();

            if (duplicatedRubrics.Any())
                Console.WriteLine($"Duplicated rubrics found for EmployeeId: {employee.EmployeeId}");
        }

        _calculateRubricHours.CalculateTotalWorkedHours(bsd);
    }
}