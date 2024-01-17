using Bsd.Domain.Enums;
using Bsd.Domain.Service;
namespace Bsd.Domain.Entities
{
    public class BsdEntity
    {
        public string BsdNumber { get; set; }
        public IEnumerable<Employee> Employee { get; }
        public DateTime DateService { get; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DayType DayType { get; }
        public List<RubricConfiguration> RubricConfigurations { get; set; } = new();

        public BsdEntity(string bsdNumber,
                   IEnumerable<Employee> employees,
                   DateTime dateService)
        {
            BsdNumber = bsdNumber;
            Employee = employees;
            DateService = dateService;
        }
    }
}
