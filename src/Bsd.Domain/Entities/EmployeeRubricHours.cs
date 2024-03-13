namespace Bsd.Domain.Entities
{
    public class EmployeeRubricHours
    {
        public int EmployeeRegistration { get; set; }
        public int EmployeeDigit { get; set; }
        public string RubricCode { get; set; } = string.Empty;
        public decimal TotalHours { get; set; }
    }
}