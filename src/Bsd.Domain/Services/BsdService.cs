using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services.Interfaces;
using Bsd.Domain.Enums;
using Bsd.Domain.Entities;

namespace Bsd.Domain.Services
{
    public class BsdService : IBsdService
    {
        private readonly IDayTypeChecker _dayTypeChecker;
        private readonly IBsdRepository _bsdRepository;
        private readonly IStaticDataService _staticDataService;

        public BsdService(IDayTypeChecker dayTypeChecker,
                          IBsdRepository bsdRepository,
                          IStaticDataService staticDataService)
        {
            _dayTypeChecker = dayTypeChecker;
            _bsdRepository = bsdRepository;
            _staticDataService = staticDataService;
        }

        public async Task<bool> CreateBsdAsync(BsdEntity bsd)
        {
            var day = _dayTypeChecker.GetDayType(bsd.DateService);

            bsd.DayType = day;
            bsd.EmployeeRubricAssignments = AddEmployeeRubricAssignmentToBsd(bsd);

            return await _bsdRepository.CreateBsdAsync(bsd);
        }

        private List<EmployeeRubricAssignment> AddEmployeeRubricAssignmentToBsd(BsdEntity bsd)
        {
            var employeeRubricAssignments = new List<EmployeeRubricAssignment>();
            var day = _dayTypeChecker.GetDayType(bsd.DateService);

            foreach (var employee in bsd.Employees)
            {
                var allowedRubrics = GetAllowedRubrics(employee, day);
                var assignment = new EmployeeRubricAssignment
                {
                    EmployeeId = employee.EmployeeId,
                    AllowedRubrics = allowedRubrics
                };
                employeeRubricAssignments.Add(assignment);
            }

            return employeeRubricAssignments;
        }

        private List<Rubric> GetAllowedRubrics(Employee employee, DayType dayType)
        {
            var rubrics = _staticDataService.GetRubrics();

            return dayType switch
            {
                DayType.Workday => rubrics
                                        .Where(r => r.ServiceType == employee.ServiceType && r.DayType == DayType.Workday)
                                        .ToList(),
                DayType.Holiday => rubrics
                                        .Where(r => r.ServiceType == employee.ServiceType && r.DayType == DayType.Holiday)
                                        .ToList(),
                DayType.Sunday => rubrics
                                        .Where(r => r.ServiceType == employee.ServiceType && r.DayType == DayType.Sunday)
                                        .ToList(),
                _ => new List<Rubric>(),
            };
        }
    }
}