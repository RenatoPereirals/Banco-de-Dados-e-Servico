using Bsd.Domain.Entities;
using Bsd.Domain.Services.Interfaces;

namespace Bsd.Infrastructure.Data
{
    public class DataService : IStaticDataService
    {
        public Employee GetEmployeeById(int employeeId)
        {
            return StaticData.Employees.FirstOrDefault(e => e.EmployeeId == employeeId)!;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return StaticData.Employees ?? Enumerable.Empty<Employee>();
        }

        public Rubric GetRubricById(int rubricId)
        {
            return StaticData.Rubrics.FirstOrDefault(e => e.RubricId == rubricId)!;
        }

        public IEnumerable<Rubric> GetRubrics()
        {
            return StaticData.Rubrics ?? Enumerable.Empty<Rubric>();
        }
    }
}