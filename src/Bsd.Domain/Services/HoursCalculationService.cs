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
           return await _rubricCalculator.CombineRubricsList(employeeId, bsdId);             
        }
    }
}
