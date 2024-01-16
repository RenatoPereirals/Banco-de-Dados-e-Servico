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

        public async Task<List<Rubric>> CombineRubricsList(string employeeId, string bsdId)
        {
            var rubricsDayType = await GetRubricsForDayType(bsdId);
            var rubricsServiceType = await GetRubricsForServiceType(employeeId);

            var rubricsSet = new HashSet<Rubric>(rubricsDayType);

            rubricsSet.UnionWith(rubricsServiceType);

            return rubricsSet.ToList();
        }

        private async Task<List<Rubric>> GetRubricsForDayType(string bsdId)
        {
            var bsd = await _bsdRepository.GetBsdByIdAsync(bsdId);
            var dayTypeRubricsRubrics = new List<Rubric>();

            var rubricsArray = await _rubricRepository.GetRubricsByDayTypeAsync(bsd.DayType);

            if (rubricsArray != null)
                dayTypeRubricsRubrics.AddRange(rubricsArray);

            return dayTypeRubricsRubrics;
        }

        private async Task<List<Rubric>> GetRubricsForServiceType(string employeeId)
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