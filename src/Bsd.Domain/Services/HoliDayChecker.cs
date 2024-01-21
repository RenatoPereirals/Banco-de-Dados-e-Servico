using Bsd.Domain.Service.Interfaces;

namespace Bsd.Domain.Entities
{
    public class HoliDayChecker : IHoliDayChecker
    {
        
        private readonly IVariableDateHolidayAdjuster _variableDateHolidayAdjuster;

        public HoliDayChecker(IVariableDateHolidayAdjuster variableDateHolidayAdjuster)
        {
            _variableDateHolidayAdjuster = variableDateHolidayAdjuster;
        }

        // Lista de feriados fixos (mês, dia)
        private static readonly List<(int, int)> FixedHolidays = new()
        {
            (1, 1),  (3, 6), (4, 7), (4, 21), (5, 1), (6, 24),
            (7, 16), (9, 7), (10, 12), (11, 2), (11, 15), (12, 8), (12, 25)
        };

        public bool IsHoliday(DateTime date)
        {
            bool isFixedHoliday = FixedHolidays.Any(h => h.Item1 == date.Month && h.Item2 == date.Day);
            bool isVariableHolidays = _variableDateHolidayAdjuster.IsVariableHoliday(date);

            return isFixedHoliday || isVariableHolidays;
        }
    }
}