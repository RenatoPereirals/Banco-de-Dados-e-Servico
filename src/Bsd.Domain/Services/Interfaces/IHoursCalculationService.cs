using Bsd.Domain.Entities;

namespace Bsd.Domain.Services.Interfaces
{
    public interface IHoursCalculationService
    {
        Task<List<Rubric>> CalculateOvertimeHoursList(string employeeId, string bsdId);
    }
}
