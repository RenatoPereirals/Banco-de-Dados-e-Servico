using Bsd.Domain.Entities;
using Bsd.Domain.Services.Interfaces;

namespace Bsd.Infrastructure.Data
{
    public class DataService : IStaticDataService
    {
        public Task<Employee> GetEmployeeById(int employeeId)
        {
            var employee = StaticData.Employees.FirstOrDefault(e => e.EmployeeId == employeeId) ??
                throw new ArgumentException($"Funcionário com mátricula {employeeId} não encontrado.");
            return Task.FromResult(employee);
        }

        public ICollection<Employee> GetEmployees()
        {
            var employees = StaticData.Employees;
            return employees;
        }

        public Rubric GetRubricById(int rubricId)
        {
            return StaticData.Rubrics.FirstOrDefault(e => e.RubricId == rubricId)!;
        }

        public Task<IEnumerable<Rubric>> GetRubrics()
        {
            var rubrics = StaticData.Rubrics ?? Enumerable.Empty<Rubric>();
            return Task.FromResult(rubrics);
        }
    }
}