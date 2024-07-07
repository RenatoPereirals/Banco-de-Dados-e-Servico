using System.ComponentModel.DataAnnotations;

namespace Bsd.Domain.Entities
{
    public class EmployeeRubricAssignment
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public List<Rubric> AllowedRubrics { get; set; } = new();
    }
}