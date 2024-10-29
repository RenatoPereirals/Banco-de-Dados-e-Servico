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

        _calculateRubricHours.CalculateTotalWorkedHours(bsd);
    }
}