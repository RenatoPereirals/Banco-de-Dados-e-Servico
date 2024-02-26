using Bsd.Domain.Entities;
using Bsd.Domain.Enums;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services.Interfaces;

namespace Bsd.Domain.Services
{
    public class BsdService : IBsdService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRubricService _rubricService;
        private readonly IDayTypeChecker _daytypeChecker;
        private readonly IEmployeeService _employeeService;

        public BsdService(IEmployeeRepository employeeRepository,
                          IRubricService rubricService,
                          IDayTypeChecker dayTypeChecker,
                          IEmployeeService employeeService)
        {
            _employeeRepository = employeeRepository;
            _rubricService = rubricService;
            _daytypeChecker = dayTypeChecker;
            _employeeService = employeeService;
        }

        public async Task<BsdEntity> CreateBsdAsync(int bsdNumber, DateTime dateService, int employeeRegistration, int digit)
        {
            _ = _employeeRepository.GetEmployeeByRegistrationAsync(employeeRegistration)
                ?? throw new ArgumentException("Mátricula do funcionário não encontrada.");

            var currentDigit = _employeeService.CalculateModulo11CheckDigit(employeeRegistration);

            if (currentDigit != digit)
                throw new Exception("O digito está incorreto.");

            var dayType = _daytypeChecker.GetDayType(dateService);
            var bsdEntity = new BsdEntity(bsdNumber, dateService)
            {
                DayType = dayType
            };

            await AddEmployeesToBsdAsync(bsdEntity, employeeRegistration, digit);

            return bsdEntity;
        }

        public async Task AddEmployeesToBsdAsync(BsdEntity bsdEntity, int employeeRegistration, int digit)
        {
            var employee = await _employeeRepository.GetEmployeeByRegistrationAsync(employeeRegistration)
                ?? throw new Exception("Mátricula do funcionário não encontrada.");

            var currentDigit = _employeeService.CalculateModulo11CheckDigit(employeeRegistration);

            if (currentDigit != digit)
                throw new Exception("O digito está incorreto.");

            var employeeBsdEntity = new EmployeeBsdEntity(employeeRegistration, employee, bsdEntity.BsdNumber, bsdEntity);
            bsdEntity.EmployeeBsdEntities.Add(employeeBsdEntity);

            await AssignRubricsToEmployeeByServiceTypeAndDayAsync(employeeBsdEntity, bsdEntity.DayType);
        }

        public async Task AssignRubricsToEmployeeByServiceTypeAndDayAsync(EmployeeBsdEntity employeeBsdEntity, DayType dayType)
        {
            var employee = employeeBsdEntity.Employee;
            var filteredRubrics = await _rubricService.FilterRubricsByServiceTypeAndDayAsync(employee.ServiceType, dayType);
            employeeBsdEntity.Rubrics = filteredRubrics;
        }
    }
}