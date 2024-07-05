using Bsd.Application.Helpers.Interfaces;

using Bsd.Domain.Repository.Interfaces;

namespace Bsd.API.Helpers
{

    public class EmployeeValidationService : IEmployeeValidationService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeValidationService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> ValidateEmployeeRegistrationAsync(int employeeRegistrations)
        {
            var employee = await _employeeRepository.GetEmployeeByRegistrationAsync(employeeRegistrations);
            if (employee == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> ValidateEmployeeRegistrationsAsync(List<int> employeeRegistrations)
        {
            foreach (var registration in employeeRegistrations)
            {
                await ValidateEmployeeRegistrationAsync(registration);
            }
            return true;
        }
    }
}

