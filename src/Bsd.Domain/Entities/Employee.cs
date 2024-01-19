using Bsd.Domain.Enums;

namespace Bsd.Domain.Entities
{
    public class Employee
    {
        public Employee(int registration, 
                        ServiceType serviceType,
                        IEnumerable<EmployeeBsdEntity> employeeBsdEntities)
        {
            SetRegistration(registration);
            ServiceType = serviceType;
            EmployeeBsdEntities = employeeBsdEntities.ToList();
        }

        public int Registration { get; private set; }
        public int Digit => CalculateModulo11CheckDigit();
        public ServiceType ServiceType { get; } = new ServiceType();
        public DateTime DateService { get; set; }
        public ICollection<EmployeeBsdEntity> EmployeeBsdEntities { get; set; }

        private void SetRegistration(int value)
        {
            ValidateRegistration(value);
            Registration = value;
        }

        private int CalculateModulo11CheckDigit()
        {
            int sum = 0;
            string registration = Registration.ToString();
            int weight = registration.Length + 1;

            foreach (char digitChar in registration)
            {
                if (!int.TryParse(digitChar.ToString(), out int digit))
                {
                    throw new FormatException($"Caractere inválido {digitChar} na mátricula");
                }

                sum += digit * weight;
                weight--;
            }

            int mod = sum % 11;
            return 11 - mod;
        }

        private static void ValidateRegistration(int value)
        {
            string registration = value.ToString();
            if (registration.Length != 4)
            {
                throw new ArgumentException("A matrícula deve conter 4 dígitos");
            }
        }
    }
}
