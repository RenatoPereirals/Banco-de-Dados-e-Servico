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

    public async Task AssociateRubricAsync(BsdEntity bsd)
    {
        var existingRubricIds = new HashSet<int>();
        
        foreach (var employee in bsd.Employees)
        {
            var rubrics = new List<Rubric>();
        
            foreach (var workedDay in employee.WorkedDays)
            {
                var dayType = _dayTypeChecker.GetDayType(workedDay.DateEntry);
        
                var allowedRubrics = await GetAllowedRubric(employee, dayType);
        
                existingRubricIds.Clear();
                existingRubricIds.UnionWith(rubrics.Select(r => r.RubricId));
                rubrics.AddRange(allowedRubrics.Where(r => !existingRubricIds.Contains(r.RubricId)));
            }
        
            employee.Rubrics = rubrics;
        }
    }

    public async Task<ICollection<Rubric>> GetAllowedRubric(Employee employee, DayType dayType)
    {
        var rubrics = await _staticDataService.GetRubrics();

        var rubricMappings = new Dictionary<DayType, Func<IEnumerable<Rubric>>>
        {
            { DayType.Workday, () => rubrics.Where(r => r.ServiceType == employee.ServiceType && r.DayType == DayType.Workday) },
            { DayType.Holiday, () => rubrics.Where(r => r.ServiceType == employee.ServiceType && r.DayType == DayType.Holiday) },
            { DayType.Sunday, () => rubrics.Where(r => r.ServiceType == employee.ServiceType && r.DayType == DayType.Sunday) },
            { DayType.SundayAndHoliday, () => rubrics.Where(r => r.ServiceType == employee.ServiceType && r.DayType == DayType.SundayAndHoliday) },
        };

        return rubricMappings.TryGetValue(dayType, out var value) ? value.Invoke().ToList() : new List<Rubric>();
    }
}