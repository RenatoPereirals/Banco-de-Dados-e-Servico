using Bsd.Domain.Enums;
namespace Bsd.Domain.Entities
{
    public class Rubric
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal HoursPerDay { get; set; }
        public DayType DayType { get; set; }
        public ServiceType ServiceType { get; set; }

        public Rubric(string code, string description, decimal hoursPerDay, DayType dayType, ServiceType serviceType)
        {
            Code = code;
            Description = description;
            HoursPerDay = hoursPerDay;
            DayType = dayType;
            ServiceType = serviceType;
        }
    }
}