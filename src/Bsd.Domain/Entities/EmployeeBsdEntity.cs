namespace Bsd.Domain.Entities
{
    public class EmployeeBsdEntity
    {
        public int EmployeeRegistration { get; set; }
        public Employee Employee { get; set; } = new();

        public int BsdEntityNumber { get; set; }
        public BsdEntity BsdEntity { get; set; } = new();

        public List<Rubric> Rubrics { get; set; } = new();
    }
}