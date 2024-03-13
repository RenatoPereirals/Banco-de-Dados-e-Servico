namespace Bsd.Application.DTOs
{
    public class EmployeeBsdEntityDto
    {
        
        public int Registration { get; set; }
        public int Digit { get; set; }
        public string RubricCode { get; set; } = string.Empty;
        public decimal TotalHours { get; set; }
    }
}