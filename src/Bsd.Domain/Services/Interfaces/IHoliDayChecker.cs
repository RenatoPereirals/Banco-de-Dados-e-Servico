namespace Bsd.Domain.Service.Interfaces
{
    public interface IHolidayChecker
    {
        bool IsHoliday(DateTime dateTime);
        bool IsHolidayEve(DateTime date);
    }
}