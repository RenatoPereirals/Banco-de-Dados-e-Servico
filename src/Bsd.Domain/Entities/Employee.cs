using Bsd.Domain.Enums;

namespace Bsd.Domain.Entities
{
    public class Employee
    {
        public Employee(int registration,
                        ServiceType serviceType)
        {
            Registration = registration;
            ServiceType = serviceType;
        }

        public int Registration { get; private set; }
        public int Digit { get; private set; }
        public ServiceType ServiceType { get; }
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
    }
}
