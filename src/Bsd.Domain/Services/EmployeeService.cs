using Bsd.Domain.Entities;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services.Interfaces;
namespace Bsd.Domain.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly Employee _employee;
        private readonly IBsdRepository _bsdRepository;
        public EmployeeService(Employee employee,
                               IBsdRepository bsdRepository)
        {
            _employee = employee;
            _bsdRepository = bsdRepository;
        }

        public void ValidateRegistration(int registrationValue)
        {
            string registration = registrationValue.ToString();
            if (registration.Length != 4)
            {
                throw new ArgumentException($"A matrícula {registrationValue}, deve conter 4 dígitos");
            }
        }

        public int CalculateModulo11CheckDigit(int registrationValue)
        {
            string registration = registrationValue.ToString();
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