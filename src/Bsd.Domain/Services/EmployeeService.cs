using Bsd.Domain.Entities;
using Bsd.Domain.Repository.Interfaces;

namespace Bsd.Domain.Services
{
    public class EmployeeService
    {
        private readonly Employee _employee;
        private readonly IBsdRepository _bsdRepository;
        public EmployeeService(Employee employee,
                               IBsdRepository bsdRepository)
        {
            _employee = employee;
            _bsdRepository = bsdRepository;
        }

        public void SetRegistrationAndDigit(int value)
        {
            ValidateRegistration(value);
            _employee.SetRegistration(value);
            _employee.SetDigit(CalculateModulo11CheckDigit(value));
        }

        private static void ValidateRegistration(int value)
        {
            string registration = value.ToString();
            if (registration.Length != 4)
            {
                throw new ArgumentException($"A matrícula {value}, deve conter 4 dígitos");
            }
        }

        private static int CalculateModulo11CheckDigit(int value)
        {
            int sum = 0;
            string registration = value.ToString();
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

        public async Task<int> CalculateEmployeeWorkedDays(int employeeRegistration, DateTime startDate, DateTime endDate)
        {
            var bsdEntities = await _bsdRepository.GetAllBsdAsync();
            return bsdEntities
                .Where(bsdDate => bsdDate.DateService >= startDate && bsdDate.DateService <= endDate)
                .SelectMany(bsdEntity => bsdEntity.EmployeeBsdEntities)
                .Count(employeeBsdEntity => employeeRegistration == employeeBsdEntity.Employee.Registration);
        }

    }
}