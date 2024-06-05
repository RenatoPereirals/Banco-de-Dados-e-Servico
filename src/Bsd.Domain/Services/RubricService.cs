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

        public async Task<List<EmployeeRubricHours>> GetEmployeeRubricHoursAsync(DateTime startDate, DateTime endDate)
        {
            var bsdEntities = await _bsdRepository.GetBsdEntitiesByDateRangeAsync(startDate, endDate)
                ?? throw new Exception($"Não foi possível encontrar nenhum BSD entre as datas {startDate} e {endDate}");

            var result = new List<EmployeeRubricHours>();

            var employeeTasks = bsdEntities.SelectMany(bsdEntity => bsdEntity.Employees.Select(async employee =>
            {
                var totalDays = await _employeeService.CalculateEmployeeWorkedDays(employee.Registration, startDate, endDate);
                var rubrics = await FilterRubricsByServiceTypeAndDayAsync(employee.ServiceType, bsdEntity.DayType);

                return rubrics.Select(rubric => new EmployeeRubricHours
                {
                    EmployeeRegistration = employee.Registration,
                    RubricCode = rubric.Code,
                    TotalHours = GetRubricHour(rubric) * totalDays
                });
            }));

            foreach (var employeeRubricHoursTask in employeeTasks)
            {
                var employeeRubricHours = await employeeRubricHoursTask;
                result.AddRange(employeeRubricHours);
            }

            return result;
        }


        private async Task<IEnumerable<Rubric>> FilterRubricsByServiceTypeAndDayAsync(ServiceType serviceType, DayType dayType)
        {
            var allRubrics = await _rubricRepository.GetAllRubricsAsync();
            return allRubrics.Where(r => r.ServiceType == serviceType && r.DayType == dayType).ToList();
        }

        private static decimal GetRubricHour(Rubric rubric)
        {
            return rubric.HoursPerDay;
        }

    }
}
