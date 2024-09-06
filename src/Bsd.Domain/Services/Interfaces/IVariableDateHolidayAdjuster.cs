namespace Bsd.Domain.Services.Interfaces;

public interface IVariableDateHolidayAdjuster
{
    bool IsVariableHoliday(DateTime date);
}