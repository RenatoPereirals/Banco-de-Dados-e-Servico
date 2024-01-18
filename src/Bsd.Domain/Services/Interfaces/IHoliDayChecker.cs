namespace Bsd.Domain.Service.Interfaces
{
    public interface IHoliDayChecker
    {
        bool IsHoliday(DateTime dateTime);
    }
}