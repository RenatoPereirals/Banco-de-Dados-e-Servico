namespace Bsd.Domain.Entities
{
    public class EmployeeRubric
    {
        public int EmployeeRubricId { get; set; }

        public int BsdEntityId { get; set; }
        public BsdEntity BsdEntity { get; set; } = new();

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = new();

        public int RubricId { get; set; }
        public Rubric Rubric { get; set; } = new();
    }
}