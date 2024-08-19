using Bsd.Domain.Entities;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services.Interfaces;
namespace Bsd.Domain.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IBsdRepository _bsdRepository;
        private readonly IStaticDataService _staticDataService;

        public EmployeeService(IBsdRepository bsdRepository, IStaticDataService staticDataService)
        {
            _bsdRepository = bsdRepository;
            _staticDataService = staticDataService;
        }

        public async Task AssociateEmployeesToBsdAsync(ICollection<BsdEntity> bsdEntities, List<int> employeeIds)
        {
            var employeeTasks = employeeIds
                .Distinct()
                .Select(async id => new { Id = id, Employee = await _staticDataService.GetEmployeeById(id) })
                .ToList();

            var employees = await Task.WhenAll(employeeTasks);

            var employeeDictionary = employees.ToDictionary(x => x.Id, x => x.Employee);

            foreach (var bsd in bsdEntities)
            {
                bsd.Employees = employeeIds
                    .Where(employeeDictionary.ContainsKey)
                    .Select(id => employeeDictionary[id])
                    .ToList();
            }
        }

        public async Task<ICollection<Employee>> GetEmployeesByIds(ICollection<int> registrations)
        {
            var employees = new List<Employee>();

            foreach (var employeeId in registrations)
            {
                var employee = await _staticDataService.GetEmployeeById(employeeId);
                employees.Add(employee);
            }

            return employees;
        }

        public async Task<int> CalculateEmployeeWorkedDays(int employeeRegistration, DateTime startDate, DateTime endDate)
        {
            var bsdEntities = await _bsdRepository.GetAllBsdAsync();
            return bsdEntities
                .Where(bsdDate => bsdDate.DateService >= startDate && bsdDate.DateService <= endDate)
                .SelectMany(bsdEntity => bsdEntity.Employees)
                .Count(employee => employeeRegistration == employee.EmployeeId);
        }

        public int CalculateModulo11CheckDigit(int registrationValue)
        {
            string registration = registrationValue.ToString();
            ValidateRegistrationLength(registration);
            ValidateRegistrationCharacters(registration);

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

        private static void ValidateRegistrationCharacters(string registration)
        {
            if (!registration.All(char.IsDigit))
            {
                throw new ArgumentException($"A matrícula {registration}, deve conter apenas números.");
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