using Bsd.Domain.Service.Interfaces;

namespace Bsd.Domain.Service
{
    public class PortuaryDayAdjuster : IHolidayAdjuster
    {
        public DateTime Adjust(DateTime date)
        {
            if (date.Month == 1 && date.Day == 28)
            {
                if (date.DayOfWeek == DayOfWeek.Saturday)
                {
                    return date.AddDays(2);
                }
                else if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    return date.AddDays(1);
                }
            }
            return date;
        }
    }
}