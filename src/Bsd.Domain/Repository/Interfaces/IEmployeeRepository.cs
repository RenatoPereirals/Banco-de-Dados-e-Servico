using Bsd.Domain.Entities;

namespace Bsd.Domain.Repository.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployeeByRegistrationAsync(int employeeId);
        Task<IEnumerable<Employee>> GetAllEmployees();
    }
}