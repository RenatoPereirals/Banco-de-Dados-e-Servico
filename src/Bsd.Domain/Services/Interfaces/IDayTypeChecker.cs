using Bsd.Domain.Enums;

namespace Bsd.Domain.Services.Interfaces
{
    public interface IDayTypeChecker
    {
        Task<DayType> GetDayType(DateTime dateTime);
    }
}