using System.ComponentModel.DataAnnotations;
using Bsd.Domain.Enums;

namespace Bsd.Domain.Entities
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public ServiceType ServiceType { get; set; }
        public ICollection<WorkedDay> WorkedDays { get; set; } = new List<WorkedDay>();
        public ICollection<Rubric> Rubrics { get; set; } = new List<Rubric>();
    }
}
