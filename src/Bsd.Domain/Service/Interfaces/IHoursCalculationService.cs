using Bsd.Domain.Enums;

namespace Bsd.Domain.Services.Interfaces
{
    public interface IHoursCalculationService
    {
        Task<decimal> CalculateOvertimeHours(string registration, DateTime dateService, DateTime startTime, DateTime endTime);
        Task<decimal> CalculateOvertimeHours(string registration, DateTime dateService, DayType dayType);
    }
}
