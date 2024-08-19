using Bsd.Domain.Enums;

namespace Bsd.Domain.Entities
{
    public class BsdEntity
    {
        public DateTime DateService { get; set; }
        public DayType DayType { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
