using Bsd.Domain.Entities;

namespace Bsd.Domain.Services.Interfaces;

public interface IStaticDataService
{        
    Task<Employee> GetEmployeeById(int employeeId);
    ICollection<Employee> GetEmployees();
    Rubric GetRubricById(int rubricId);
    Task<IEnumerable<Rubric>> GetRubrics();
}