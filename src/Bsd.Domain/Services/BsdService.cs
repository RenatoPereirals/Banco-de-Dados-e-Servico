using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services.Interfaces;
using Bsd.Domain.Enums;
using Bsd.Domain.Entities;

namespace Bsd.Domain.Services
{
    public class BsdService : IBsdService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRubricRepository _rubricRepository;

        public BsdService(IEmployeeRepository employeeRepository,
                          IRubricRepository rubricRepository)
        {
            _rubricRepository = rubricRepository;
            _employeeRepository = employeeRepository;
        }
   
        public async Task<ICollection<EmployeeRubric>> AssociateRubricsToEmployeesAsync(BsdEntity bsd, DayType day)
        {
            var employeeRubricsList = new List<EmployeeRubric>();

            foreach (var employee in bsd.Employees)
            {
                if (employee != null)
                {
                    var employeeFromRepo = await _employeeRepository.GetEmployeeByRegistrationAsync(employee.EmployeeId);

                    if (employeeFromRepo != null)
                    {
                        var filteredRubrics = await _rubricRepository.GetRubricsByServiceTypeAndDayTypeAsync(employeeFromRepo.ServiceType, day);

                        foreach (var rubric in filteredRubrics)
                        {
                            employeeRubricsList.Add(new EmployeeRubric
                            {
                                BsdEntityId = bsd.BsdId,
                                BsdEntity = bsd,
                                EmployeeId = employee.EmployeeId,
                                Employee = employeeFromRepo,
                                RubricId = rubric.RubricId,
                                Rubric = rubric
                            });
                        }
                    }
                }
            }
            return employeeRubricsList;
        }
    }
}