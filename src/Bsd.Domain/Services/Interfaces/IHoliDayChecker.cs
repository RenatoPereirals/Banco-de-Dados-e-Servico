namespace Bsd.Domain.Services.Interfaces;

public interface IHolidayChecker
{
    bool IsHoliday(DateTime dateTime);
    bool IsHolidayEve(DateTime date);
}