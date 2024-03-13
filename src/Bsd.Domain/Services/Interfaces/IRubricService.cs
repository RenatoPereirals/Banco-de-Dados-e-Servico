using Bsd.Domain.Entities;
using Bsd.Domain.Enums;

namespace Bsd.Domain.Services.Interfaces
{
    public interface IRubricService
    {
        Task<IEnumerable<Rubric>> FilterRubricsByServiceTypeAndDayAsync(ServiceType serviceType, DayType dayType);
        Task<List<EmployeeRubricHours>> CalculateTotalHoursPerMonthByRubricsForEmployeeAsync(int registration, DateTime startDate, DateTime endDate);
    }
}