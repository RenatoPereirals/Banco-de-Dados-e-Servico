using Bsd.Domain.Services.Interfaces;

namespace Bsd.Domain.Entities;

public class HolidayChecker : IHolidayChecker
{

    private readonly IVariableDateHolidayAdjuster _variableDateHolidayAdjuster;

    public HolidayChecker(IVariableDateHolidayAdjuster variableDateHolidayAdjuster)
    {
        _variableDateHolidayAdjuster = variableDateHolidayAdjuster;
    }

    // List of fixed holidays (month, day)
    private static readonly List<(int, int)> FixedHolidays = new()
    {
        (1, 1),  (3, 6), (4, 7), (4, 21), (5, 1), (6, 24),
        (7, 16), (9, 7), (10, 12), (11, 2), (11, 15), (12, 8), (12, 25)
    };

    public bool IsHoliday(DateTime date)
    {
        return CheckHoliday(date);
    }

    public bool IsHolidayEve(DateTime date)
    {
        return CheckHoliday(date.AddDays(1));
    }

    private bool CheckHoliday(DateTime date)
    {
        bool isFixedHoliday = FixedHolidays.Any(h => h.Item1 == date.Month && h.Item2 == date.Day);
        bool isVariableHoliday = _variableDateHolidayAdjuster.IsVariableHoliday(date);

        return isFixedHoliday || isVariableHoliday;
    }
}