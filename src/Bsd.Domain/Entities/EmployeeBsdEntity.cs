namespace Bsd.Domain.Entities
{
    public class EmployeeBsdEntity
    {
        public int EmployeeRegistration { get; set; }
        public Employee Employee { get; set; } = new();

        public int BsdNumber { get; set; }
        public BsdEntity BsdEntity { get; set; } = new();
        public IEnumerable<Rubric> Rubrics { get; set; } = new List<Rubric>();
    }
}