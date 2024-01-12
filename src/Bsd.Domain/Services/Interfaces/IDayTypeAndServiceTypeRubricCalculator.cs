using Bsd.Domain.Entities;

namespace Bsd.Domain.Services.Interfaces
{
    public interface IDayTypeAndServiceTypeRubricCalculator
    {
        Task<List<Rubric>> CalculateOvertimeRubricsBasedOnDayType(string employeeId);
        Task<List<Rubric>> CalculateOvertimeRubricsBasedOnServiceType(string employeeId);
    }
}