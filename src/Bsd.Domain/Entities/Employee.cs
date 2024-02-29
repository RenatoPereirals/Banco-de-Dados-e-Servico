using Bsd.Domain.Enums;

namespace Bsd.Domain.Entities
{
    public class Employee
    {
        public int Registration { get; set; }
        public int Digit { get; set; }
        public ServiceType ServiceType { get; set; }
        public DateTime DateService { get; set; }
        public ICollection<EmployeeBsdEntity> EmployeeBsdEntities { get; set; } = new List<EmployeeBsdEntity>();

        public void SetRegistration(int value)
        {
            Registration = value;
        }

        public void SetDigit(int value)
        {
            Digit = value;
        }

        public void SetServiceType(ServiceType service)
        {
            ServiceType = service;
        }
    }
}
