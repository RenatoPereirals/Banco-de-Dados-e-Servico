using Bsd.Domain.Entities;

namespace Bsd.Domain.Services.Interfaces
{
    public interface IStaticDataService
    {
        Employee GetEmployeeById(int employeeId);
        IEnumerable<Employee> GetEmployees();
        Rubric GetRubricById(int rubricId);
        IEnumerable<Rubric> GetRubrics();
    }
}