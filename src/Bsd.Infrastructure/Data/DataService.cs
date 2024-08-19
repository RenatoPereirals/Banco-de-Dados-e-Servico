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

        public Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            var employees = StaticData.Employees ?? Enumerable.Empty<Employee>();
            return Task.FromResult(employees);
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