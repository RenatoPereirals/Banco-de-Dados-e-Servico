using System.Data;
using Bsd.Domain.Entities;
using Bsd.Domain.Repository.Interfaces;

namespace Bsd.Domain.Services
{
   public class BsdService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRubricRepository _rubricRepository;

        public BsdService(IEmployeeRepository employeeRepository, IRubricRepository rubricRepository)
        {
            _employeeRepository = employeeRepository;
            _rubricRepository = rubricRepository;
        }

        public async Task<Dictionary<int, List<Rubric>>> FilterRubricsBasedOnTheEmployeeTypeServiceAndTypeDay(BsdEntity bsd)
        {
            var employeeRubrics = new Dictionary<int, List<Rubric>>();
            var allRubrics = await _rubricRepository.GetAllRubricsAsync();

            foreach (var employeeId in bsd.EmployeeIds)
            {
                var employee = await _employeeRepository.GetEmployeeByRegistrationAsync(employeeId);
                var filteredRubrics = new HashSet<Rubric>(allRubrics
                    .Where(r => r.ServiceType == employee.ServiceType && r.DayType == bsd.DayType))
                    .ToList();
                employeeRubrics.Add(employeeId, filteredRubrics);
            }

            return employeeRubrics;
        }

    }
}