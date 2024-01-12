using Bsd.Domain.Entities;

namespace Bsd.Domain.Services.Interfaces
{
    public interface IDayTypeRubricCalculator
    {
        Task<List<Rubric>> CalculateOvertimeRubricsBasedOnDayType(string employeeId);
    }
}