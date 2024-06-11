using System.ComponentModel.DataAnnotations;
using Bsd.Domain.Enums;

namespace Bsd.Domain.Entities
{
    public class Rubric
    {
        [Key]
        public int RubricId { get; set; }
        public string Description { get; set; } =string.Empty;
        public decimal HoursPerDay { get; set; }
        public DayType DayType { get; set; }
        public ServiceType ServiceType { get; set; }
    }
}