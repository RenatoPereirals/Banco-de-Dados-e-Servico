using Bsd.Domain.Enums;
using Bsd.Domain.Service.Interfaces;
using Bsd.Domain.Services.Interfaces;

namespace Bsd.Domain.Services
{
    public class DayTypeChecker : IDayTypeChecker
    {
        private readonly IHoliDayChecker _holiDayChecker;
        public DayTypeChecker(IHoliDayChecker holiDayChecker)
        {
            _holiDayChecker = holiDayChecker;

        }
        public DayType GetDayType(DateTime date)
        {
            if (IsSundayAndHoliday(date))
            {
                return DayType.SundayAndHoliday;
            }
            else if (IsSunday(date))
            {
                return DayType.Sunday;
            }
            else if (_holiDayChecker.IsHoliday(date))
            {
                return DayType.HoliDay;
            }
            else
            {
                return DayType.Workday;
            }
        }

        private bool IsSundayAndHoliday(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Sunday && _holiDayChecker.IsHoliday(date))
            {
                return true;
            }
            return false;
        }

        private static bool IsSunday(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Sunday)
            {
                return true;
            }
            return false;
        }
    }
}