using Bsd.Domain.Enums;
namespace Bsd.Domain.Entities
{
    public class BsdEntity
    {
        public string BsdNumber { get; set; }
        public IEnumerable<Employee> Employee { get; }
        public List<Rubric> Rubrics { get; }
        public DateTime DateService { get; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DayType DayType { get; }

        public BsdEntity(string bsdNumber,
                   IEnumerable<Employee> employees,
                   DateTime dateService)
        {
            BsdNumber = bsdNumber;
            Employee = employees;
            DateService = dateService;
            Rubrics = employees.SelectMany(e => e.Rubrics).ToList();
        }
    }
}
