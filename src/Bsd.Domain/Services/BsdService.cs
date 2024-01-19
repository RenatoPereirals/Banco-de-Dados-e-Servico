using System.Data;
using Bsd.Domain.Entities;
using Bsd.Domain.Repository.Interfaces;

namespace Bsd.Domain.Services
{
    public class BsdService
    {
        private readonly IRubricRepository _rubricRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public BsdService(IRubricRepository rubricRepository,
                          IEmployeeRepository employeeRepository)
        {
            _rubricRepository = rubricRepository;
            _employeeRepository = employeeRepository;
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
            }
        }

        public async Task<Dictionary<int, List<Rubric>>> FilterRubricsBasedOnTheEmployeeTypeServiceAndTypeDay(BsdEntity bsd)
        {
            var employeeRubrics = new Dictionary<int, List<Rubric>>();
            var allRubrics = await _rubricRepository.GetAllRubricsAsync();

            foreach (var employeeBsdEntity in bsd.EmployeeBsdEntities)
            {
                var employee = employeeBsdEntity.Employee;
                var filteredRubrics = new HashSet<Rubric>(allRubrics
                    .Where(r => r.ServiceType == employee.ServiceType && r.DayType == bsd.DayType))
                    .ToList();
                employeeRubrics.Add(employee.Registration, filteredRubrics);
            }

            return employeeRubrics;
        }

    }
}