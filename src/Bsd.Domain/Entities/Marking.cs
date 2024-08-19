namespace Bsd.Domain.Entities
{
    public class Marking
    {
        DateOnly StartDate { get; set; }
        DateOnly EndDate { get; set; }
        TimeOnly StartTime { get; set; }
        TimeOnly EndTime { get; set; }
    }
}