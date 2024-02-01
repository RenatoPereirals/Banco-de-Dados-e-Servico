using Bsd.Domain.Entities;

namespace Bsd.Domain.Services
{
    public class EmployeeService
    {
        private readonly Employee _employee;
        public EmployeeService(Employee employee)
        {
            _employee = employee;
        }

        public void SetRegistrationAndDigit(int registration)
        {
            ValidateRegistration(registration);
            _employee.SetRegistration(registration);
        }

        private static void ValidateRegistration(int value)
        {
            string registration = value.ToString();
            if (registration.Length != 4)
            {
                throw new ArgumentException($"A matrícula {value}, deve conter 4 dígitos");
            }
        }

        public int CalculateModulo11CheckDigit(int value)
        {
            string registration = value.ToString();
            ValidateRegistrationLength(registration);

            int sum = CalculateWeightedSum(registration);
            int mod = sum % 11;
            return 11 - mod;
        }

        private static void ValidateRegistrationLength(string registration)
        {
            if (registration.Length != 4)
            {
                throw new ArgumentException($"A matrícula {registration} deve conter exatamente 4 dígitos.");
            }
        }

        private static int CalculateWeightedSum(string registration)
        {
            int sum = 0;
            int weight = registration.Length + 1;

            foreach (char digitChar in registration)
            {
                if (!int.TryParse(digitChar.ToString(), out int digit))
                    throw new FormatException($"O caractere {digitChar} é inválido na matrícula");

                sum += digit * weight;
                weight--;
            }

            return sum;
        }
    }
}