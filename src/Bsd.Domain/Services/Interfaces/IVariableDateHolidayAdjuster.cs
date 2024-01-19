namespace Bsd.Domain.Service.Interfaces
{
    public interface IVariableDateHolidayAdjuster
    {
        bool IsVariableHoliday(DateTime date);
    }
}