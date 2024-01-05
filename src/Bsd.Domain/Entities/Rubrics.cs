using Bsd.Domain.Enums;

namespace Bsd.Domain.Entities
{
    public class Rubrics
    {
        public string Rubric { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int HoursPerDay { get; set; }
        public double Percentage { get; set; }
        public DayType DayType { get; set; }  
    }
}