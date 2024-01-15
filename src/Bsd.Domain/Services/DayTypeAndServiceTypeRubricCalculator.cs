using Bsd.Domain.Entities;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services.Interfaces;

namespace Bsd.Domain.Services
{
    public class DayTypeAndServiceTypeRubricCalculator : IDayTypeAndServiceTypeRubricCalculator
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRubricRepository _rubricRepository;
        private readonly IBsdRepository _bsdRepository;

        public DayTypeAndServiceTypeRubricCalculator(
                                    IEmployeeRepository employeeRepository,
                                    IRubricRepository rubricRepository,
                                    IBsdRepository bsdRepository)
        {
            _employeeRepository = employeeRepository;
            _rubricRepository = rubricRepository;
            _bsdRepository = bsdRepository;
        }

        public async Task<List<Rubric>> CalculateOvertimeRubricsBasedOnDayType(string bsdId)
        {
            var bsd = await _bsdRepository.GetBsdByIdAsync(bsdId);
            var dayTypeRubricsRubrics = new List<Rubric>();

            var rubricsArray = await _rubricRepository.GetRubricsByDayTypeAsync(bsd.DayType);

            if (rubricsArray != null)
                dayTypeRubricsRubrics.AddRange(rubricsArray);

            return dayTypeRubricsRubrics;
        }

        public async Task<List<Rubric>> CalculateOvertimeRubricsBasedOnServiceType(string employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeByRegistrationAsync(employeeId);
            var serviceTypeRubrics = new List<Rubric>();

            var rubricsArray = await _rubricRepository.GetRubricsByServiceTypeAsync(employee.ServiceType);

            if (rubricsArray != null)
                serviceTypeRubrics.AddRange(rubricsArray);

            return serviceTypeRubrics;
        }
    }
}