using Bsd.Domain.Service.Interfaces;
using Bsd.Domain.Services.Interfaces;
using Bsd.Domain.Enums;

namespace Bsd.Domain.Services
{
    public class DayTypeChecker : IDayTypeChecker
    {
        private readonly IHolidayChecker _holidayChecker;
        public DayTypeChecker(IHolidayChecker holidayChecker)
        {
            _holidayChecker = holidayChecker;
        }

        public Task<DayType> GetDayType(DateTime date)
        {
            if (IsSundayAndHoliday(date))
            {
                return Task.FromResult(DayType.SundayAndHoliday);
            }
            else if (IsSunday(date))
            {
                return Task.FromResult(DayType.Sunday);
            }
            else if (_holidayChecker.IsHoliday(date))
            {
                return Task.FromResult(DayType.Holiday);
            }
            else
            {
                return Task.FromResult(DayType.Workday);
            }
        }


        private bool IsSundayAndHoliday(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Sunday && _holidayChecker.IsHoliday(date))
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