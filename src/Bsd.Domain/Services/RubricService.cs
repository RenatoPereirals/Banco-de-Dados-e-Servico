using Bsd.Domain.Entities;
using Bsd.Domain.Enums;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services.Interfaces;

namespace Bsd.Domain.Services
{
    public class RubricService : IRubricService
    {
        private readonly IRubricRepository _rubricRepository;
        private readonly IBsdRepository _bsdRepository;
        private readonly IEmployeeService _employeeService;

        public RubricService(IRubricRepository rubricRepository,
                             IBsdRepository bsdRepository,
                             IEmployeeService employeeService)
        {
            _rubricRepository = rubricRepository;
            _bsdRepository = bsdRepository;
            _employeeService = employeeService;
        }

        public async Task<List<Rubric>> FilterRubricsByServiceTypeAndDayAsync(ServiceType serviceType, DayType dayType)
        {
            var allRubrics = await _rubricRepository.GetAllRubricsAsync();
            return allRubrics.Where(r => r.ServiceType == serviceType && r.DayType == dayType).ToList();
        }

        public async Task<List<EmployeeRubricHours>> CalculateTotalHoursPerMonthByRubrics(DateTime startDate, DateTime endDate)
        {
            var employeeBsdEntities = await _bsdRepository.GetEmployeeBsdEntitiesByDateRangeAsync(startDate, endDate);

            var result = new List<EmployeeRubricHours>();

            foreach (var employeeBsdEntity in employeeBsdEntities)
            {
                var totalDays = await _employeeService.CalculateEmployeeWorkedDays(employeeBsdEntity.Employee.Registration, startDate, endDate);

                foreach (var rubric in employeeBsdEntity.Rubrics)
                {
                    result.Add(new EmployeeRubricHours
                    {
                        EmployeeRegistration = employeeBsdEntity.Employee.Registration,
                        RubricCode = rubric.Code,
                        TotalHours = GetRubricHour(rubric) * totalDays
                    });
                }
            }

            return result;
        }

        private static decimal GetRubricHour(Rubric rubric)
        {
            var rubricHour = rubric.HoursPerDay;
            return rubricHour;
        }
    }
}
