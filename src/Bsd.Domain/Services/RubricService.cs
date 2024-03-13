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

        public async Task<IEnumerable<Rubric>> FilterRubricsByServiceTypeAndDayAsync(ServiceType serviceType, DayType dayType)
        {
            var allRubrics = await _rubricRepository.GetAllRubricsAsync();
            return allRubrics.Where(r => r.ServiceType == serviceType && r.DayType == dayType).ToList();
        }

        public async Task<List<EmployeeRubricHours>> CalculateTotalHoursPerMonthByRubricsForEmployeeAsync(int registration, DateTime startDate, DateTime endDate)
        {
            IEnumerable<BsdEntity> bsdEntities = await _bsdRepository.GetEmployeeBsdEntitiesByDateRangeAsync(startDate, endDate);
            var result = new List<EmployeeRubricHours>();
            int totalDays = await _employeeService.CalculateEmployeeWorkedDays(registration, startDate, endDate);
            IEnumerable<Rubric> rubrics = GetRubricsInEmployeeBsdEntity(bsdEntities);

            foreach (var bsdEntity in bsdEntities)
            {
                foreach (var rubric in rubrics)
                {
                    result.Add(new EmployeeRubricHours
                    {
                        EmployeeRegistration = registration,
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

        private IEnumerable<Rubric> GetRubricsInEmployeeBsdEntity(IEnumerable<BsdEntity> employeeBsdEntities)
        {
            IEnumerable<EmployeeBsdEntity> employeeBsds = GetEmployeeBsdEntitiesInBsdEntity(employeeBsdEntities);
            var rubricList = new List<Rubric>();

            foreach (var employeeBsdEntity in employeeBsds)
            {
                rubricList.AddRange(employeeBsdEntity.Rubrics);
            }
            return rubricList;
        }

        private IEnumerable<EmployeeBsdEntity> GetEmployeeBsdEntitiesInBsdEntity(IEnumerable<BsdEntity> bsdEntities)
        {
            var employeeBsdEntities = new List<EmployeeBsdEntity>();

            foreach (var bsdEntity in bsdEntities)
            {
                employeeBsdEntities.AddRange(bsdEntity.EmployeeBsdEntities);
            }
            return employeeBsdEntities;
        }
    }
}
