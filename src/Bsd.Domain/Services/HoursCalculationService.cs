using Bsd.Domain.Entities;
using Bsd.Domain.Services.Interfaces;

namespace Bsd.Domain.Services
{
    public class HoursCalculationService : IHoursCalculationService
    {
        private readonly IDayTypeAndServiceTypeRubricCalculator _rubricCalculator;

        public HoursCalculationService(IDayTypeAndServiceTypeRubricCalculator dayTypeRubricCalculator)
        {
            _rubricCalculator = dayTypeRubricCalculator;
        }

        public async Task<List<Rubric>> CalculateOvertimeHoursList(string employeeId, string bsdId)
        {
            var dayTypeRubrics = await _rubricCalculator.CalculateOvertimeRubricsBasedOnDayType(employeeId);
            var serviceTypeRubrics = await _rubricCalculator.CalculateOvertimeRubricsBasedOnServiceType(bsdId);

            var combinedRubrics = CombineRubrics(serviceTypeRubrics, dayTypeRubrics);

            return combinedRubrics;
        }

        private List<Rubric> CombineRubrics(List<Rubric> rubrics1, List<Rubric> rubrics2)
        {
            var rubricsSet = new HashSet<Rubric>(rubrics1); // HashSet garante unicidade

            rubricsSet.UnionWith(rubrics2); // Adiciona rubricas do segundo conjunto

            return rubricsSet.ToList();
        }
    }
}
