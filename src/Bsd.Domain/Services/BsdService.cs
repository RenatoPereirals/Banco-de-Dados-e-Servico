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
        private readonly IBsdRepository _bsdReposiory;

        public BsdService(IEmployeeRepository employeeRepository,
                          IRubricService rubricService,
                          IDayTypeChecker dayTypeChecker,
                          IBsdRepository bsdRepository)
        {
            _employeeRepository = employeeRepository;
            _rubricService = rubricService;
            _daytypeChecker = dayTypeChecker;
            _bsdReposiory = bsdRepository;
        }

        public async Task<BsdEntity> CreateBsdAsync(int bsdNumber, DateTime dateService, List<int> employeeRegistrations)
        {
            var dayType = _daytypeChecker.GetDayType(dateService);
            var bsdEntity = new BsdEntity(bsdNumber, dateService)
            {
                DayType = dayType
            };

            foreach (var employeeRegistration in employeeRegistrations)
            {
                await AddEmployeesToBsdAsync(bsdEntity, employeeRegistration);
            }
            return bsdEntity;
        }

        public async Task AddEmployeesToBsdAsync(BsdEntity bsdEntity, int employeeRegistrations)
        {
            var employee = await _employeeRepository.GetEmployeeByRegistrationAsync(employeeRegistrations)
                ?? throw new Exception("Mátricula do funcionário não encontrada.");

            var employeeBsdEntity = new EmployeeBsdEntity(employeeRegistrations, employee, bsdEntity.BsdNumber, bsdEntity);
            bsdEntity.EmployeeBsdEntities.Add(employeeBsdEntity);

            await AssignRubricsToEmployeeByServiceTypeAndDayAsync(employeeBsdEntity, bsdEntity.DayType);
        }

        public async Task AssignRubricsToEmployeeByServiceTypeAndDayAsync(EmployeeBsdEntity employeeBsdEntity, DayType dayType)
        {
            var employee = employeeBsdEntity.Employee;
            var filteredRubrics = await _rubricService.FilterRubricsByServiceTypeAndDayAsync(employee.ServiceType, dayType);
            employeeBsdEntity.Rubrics = filteredRubrics;
        }

        public async Task<int> CalculateEmployeeWorkedDays(int employeeId, DateTime startDate, DateTime endDate)
        {
            var bsdEntities = await _bsdReposiory.GetAllBsdAsync();
            var employee = await _employeeRepository.GetEmployeeByRegistrationAsync(employeeId);

            var filterbsdEntities = bsdEntities.Where(bsdDate => bsdDate.DateService >= startDate && bsdDate.DateService <= endDate);

            var workedDays = 0;

            foreach (var bsdEntity in filterbsdEntities)
            {
                foreach (var employeeBsdEntity in bsdEntity.EmployeeBsdEntities)
                {
                    if (employee.Registration == employeeBsdEntity.Employee.Registration)
                    {
                        workedDays++;
                    }
                }
            }
            return workedDays;
        }
    }
}