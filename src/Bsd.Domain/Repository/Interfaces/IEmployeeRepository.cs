using Bsd.Domain.Entities;

namespace Bsd.Domain.Repository.Interfaces
{
    public interface IEmployeeRepository : IGeralRepository<Employee>
    {
        Task<Employee> GetEmployeeByRegistrationAsync(int employeeId);
        IEnumerable<Employee> GetAllEmployees();
        void Delete(Employee employee);
    }
}