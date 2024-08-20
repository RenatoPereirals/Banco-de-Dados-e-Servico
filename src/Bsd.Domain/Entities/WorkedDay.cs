namespace Bsd.Domain.Entities
{
    public class WorkedDay
    {
        public DateTime DateEntry { get; set; }
        public DateTime DateExit { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}