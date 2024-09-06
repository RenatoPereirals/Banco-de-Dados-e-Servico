using Bsd.Domain.Enums;

namespace Bsd.Domain.Services.Interfaces;

public interface IDayTypeChecker
{
    DayType GetDayType(DateTime dateTime);
}