using Bsd.Domain.Entities;
using Bsd.Domain.Enums;

namespace Bsd.Domain.Services.Interfaces
{
    public interface IRubricService
    {
        Task<List<Rubric>> FilterRubricsByServiceTypeAndDayAsync(ServiceType serviceType, DayType dayType);
        Task<List<EmployeeRubricHours>> CalculateTotalHoursPerMonthByRubrics(DateTime startDate, DateTime endDate);
    }
}