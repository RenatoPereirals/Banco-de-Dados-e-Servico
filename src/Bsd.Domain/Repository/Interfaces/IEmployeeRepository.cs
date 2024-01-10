using Bsd.Domain.Entities;

namespace Bsd.Domain.Repository.Interfaces
{
    public interface IEmployeeRepository : ICreatableUpdatable<Employee>
    {
        Task<Employee> GetEmployeeByRegistrationAsync(string employeeId);
        IEnumerable<Employee> GetAllEmployees();
        void Delete(Employee employee);
    }
}