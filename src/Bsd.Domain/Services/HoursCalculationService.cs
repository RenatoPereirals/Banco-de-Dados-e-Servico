using Bsd.Domain.Entities;
using Bsd.Domain.Exceptions.Interfaces;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services.Interfaces;

namespace Bsd.Domain.Services
{
    public class HoursCalculationService : IHoursCalculationService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDayTypeRubricCalculator _dayTypeRubricCalculator;
        private readonly IEmployeeException _employeeException;

        public HoursCalculationService(IEmployeeRepository employeeRepository,
                                          IDayTypeRubricCalculator dayTypeRubricCalculator,
                                          IEmployeeException employeeException)
        {
            _employeeRepository = employeeRepository;
            _dayTypeRubricCalculator = dayTypeRubricCalculator;
            _employeeException = employeeException;
        }

        public async Task<List<Rubric>> CalculateOvertimeHoursList(string employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeByRegistrationAsync(employeeId);
            _employeeException.ValidateEmployeeIdNotNull(employee);
            
            var listRubrics = await _dayTypeRubricCalculator.CalculateOvertimeRubricsBasedOnDayType(employeeId);

            return listRubrics;
        }        
    }
}
