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

        public BsdService(IEmployeeRepository employeeRepository,
                          IRubricService rubricService)
        {
            _employeeRepository = employeeRepository;
            _rubricService = rubricService;
        }

        public async Task<BsdEntity> CreateBsdAsync(int bsdNumber, DateTime dateService, IEnumerable<int> employeeRegistrations)
        {
            var bsdEntity = new BsdEntity(bsdNumber, dateService);
            await AddEmployeesToBsdAsync(bsdEntity, employeeRegistrations);
            return bsdEntity;
        }

        private async Task AddEmployeesToBsdAsync(BsdEntity bsdEntity, IEnumerable<int> employeeRegistrations)
        {
            foreach (var registration in employeeRegistrations)
            {
                var employee = await _employeeRepository.GetEmployeeByRegistrationAsync(registration)
                    ?? throw new Exception("Usuário não pode ser nulo.");

                var employeeBsdEntity = new EmployeeBsdEntity(registration, employee, bsdEntity.BsdNumber, bsdEntity);
                bsdEntity.EmployeeBsdEntities.Add(employeeBsdEntity);

                await AssignRubricsToEmployeeByServiceTypeAndDayAsync(employeeBsdEntity, bsdEntity.DayType);
            }
        }

        private async Task AssignRubricsToEmployeeByServiceTypeAndDayAsync(EmployeeBsdEntity employeeBsdEntity, DayType dayType)
        {
            var employee = employeeBsdEntity.Employee;
            var filteredRubrics = await _rubricService.FilterRubricsByServiceTypeAndDayAsync(employee.ServiceType, dayType);
            employeeBsdEntity.Rubrics = filteredRubrics;
        }
    }
}