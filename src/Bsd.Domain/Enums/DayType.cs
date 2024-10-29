namespace Bsd.Domain.Enums
{
    [Flags]
    public enum DayType
    {
        None = 0,
        Workday = 1 << 0,
        Sunday = 1 << 1,
        Holiday = 1 << 2,
        SundayAndHoliday = Sunday | Holiday,
        AllDays = Sunday | Holiday | Workday
    }
}