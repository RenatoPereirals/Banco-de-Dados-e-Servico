using Bsd.Domain.Entities;
using Bsd.Domain.Exceptions.Interfaces;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services.Interfaces;

namespace Bsd.Domain.Services
{
    public class HoursCalculationService : IHoursCalculationService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDayTypeAndServiceTypeRubricCalculator _rubricCalculator;
        private readonly IEmployeeException _employeeException;

        public HoursCalculationService(IEmployeeRepository employeeRepository,
                                          IDayTypeAndServiceTypeRubricCalculator dayTypeRubricCalculator,
                                          IEmployeeException employeeException)
        {
            _employeeRepository = employeeRepository;
            _rubricCalculator = dayTypeRubricCalculator;
            _employeeException = employeeException;
        }

        public async Task<List<Rubric>> CalculateOvertimeHoursList(string employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeByRegistrationAsync(employeeId);
            _employeeException.ValidateEmployeeIdNotNull(employee);
            
            var listRubrics = await _rubricCalculator.CalculateOvertimeRubricsBasedOnDayType(employeeId);

            return listRubrics;
        }        
    }
}
