using Bsd.Domain.Services.Interfaces;
using Bsd.Domain.Entities;
using Bsd.Domain.Enums;

namespace Bsd.Domain.Services;

public class RubricService : IRubricService
{
    private readonly IStaticDataService _staticDataService;
    private readonly IDayTypeChecker _dayTypeChecker;

    public RubricService(IStaticDataService staticDataService, IDayTypeChecker dayTypeChecker)
    {
        _staticDataService = staticDataService;
        _dayTypeChecker = dayTypeChecker;
    }

    public async Task AssociateRubricsToEmployeeAsync(BsdEntity bsdEntity)
    {
        if (bsdEntity.Employees == null || !bsdEntity.Employees.Any())
            throw new InvalidOperationException("No employees found in BSD entity.");

        foreach (var employee in bsdEntity.Employees)
        {
            var rubrics = new List<Rubric>();

            if (employee.WorkedDays == null || employee.WorkedDays.Count == 0)
                throw new Exception("Employee does not have worked days");
            else
            {
                foreach (var workedDay in employee.WorkedDays)
                {
                    var dayType = _dayTypeChecker.GetDayType(workedDay.DateEntry);

                    var allowedRubrics = await GetAllowedRubric(employee.ServiceType, dayType);

                    rubrics.AddRange(allowedRubrics);
                }
            }
            employee.Rubrics = rubrics;
        }
    }

    public async Task<ICollection<Rubric>> GetAllowedRubric(ServiceType serviceType, DayType dayType)
    {
        var rubrics = await _staticDataService.GetRubrics();

        var result = rubrics
            .Where(r => r.ServiceType.HasFlag(serviceType) && r.DayType.HasFlag(dayType))
            .ToList();

        return result;
    }
}