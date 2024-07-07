using Bsd.Domain.Enums;

using System.ComponentModel.DataAnnotations;

namespace Bsd.Domain.Entities
{
    public class BsdEntity
    {
        public BsdEntity()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        public int BsdId { get; set; }
        public DateTime DateService { get; set; }
        public DayType DayType { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public List<EmployeeRubricAssignment> EmployeeRubricAssignments { get; set; } = new();
    }
}
