using Bsd.Domain.Service.Interfaces;

namespace Bsd.Domain.Entities
{
    public class HoliDayChecker : IHoliDayChecker
    {
        // Lista de feriados fixos (mês, dia)
        private static readonly List<(int, int)> FixedHolidays = new()
        {
            (1, 1), (1, 28), (3, 6), (4, 7), (4, 21), (5, 1), (6, 24), 
            (7, 16), (9, 7), (10, 12), (11, 2), (11, 15), (12, 8), (12, 25)
        };

        private readonly List<IHolidayAdjuster> _holidayAdjusters;

        public HoliDayChecker(List<IHolidayAdjuster> holidayAdjusters)
        {
            _holidayAdjusters = holidayAdjusters;
        }

        public bool IsHoliday(DateTime dateTime)
        {
            // Verifica se a data está na lista de feriados fixos
            bool isFixedHoliday = FixedHolidays.Any(h => h.Item1 == dateTime.Month && h.Item2 == dateTime.Day);

            // Aplica ajustes adicionais para feriados específicos
            DateTime adjustedDate = ApplyAdjustments(dateTime);

            // Verifica novamente se a data ajustada é feriado
            bool isAdjustedHoliday = FixedHolidays.Any(h => h.Item1 == adjustedDate.Month && h.Item2 == adjustedDate.Day);

            return isFixedHoliday || isAdjustedHoliday;
        }

        private DateTime ApplyAdjustments(DateTime date)
        {
            // Aplica ajustes adicionais para feriados específicos
            foreach (var adjuster in _holidayAdjusters)
            {
                date = adjuster.Adjust(date);
            }

            return date;
        }
    }
}