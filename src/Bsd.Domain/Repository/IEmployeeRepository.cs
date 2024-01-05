using Bsd.Domain.Entities;

namespace Bsd.Domain.Repository
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployeeByRegistrationAsync(string employeeId);
    }
}