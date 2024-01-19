using Bsd.Domain.Service.Interfaces;

namespace Bsd.Domain.Service
{
    public class VariableDateHolidayAdjuster : IVariableDateHolidayAdjuster
    {
        public bool IsVariableHoliday(DateTime date)
        {
            return IsPortuaryDay(date);
        }

        private bool IsPortuaryDay(DateTime date)
        {
            if (date.Month == 1 && (date.Day == 28 || date.Day == 29 || date.Day == 30))
            {
                if (date.Day == 28 && (
                    date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday))
                {
                    return false;
                }
                if ((date.Day == 29 || date.Day == 30) && date.DayOfWeek != DayOfWeek.Monday)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}