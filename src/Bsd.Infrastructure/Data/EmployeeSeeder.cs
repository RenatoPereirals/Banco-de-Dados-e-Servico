using Bsd.Domain.Entities;
using Bsd.Domain.Enums;
using Bsd.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Bsd.Infrastructure.Data
{
    public class EmployeeSeeder
    {
        public void Seed(BsdDbContext context, Action<DbSet<Employee>, List<Employee>> addEntitiesIfNotExists)
        {
            var employees = GetEmployees();
            addEntitiesIfNotExists(context.Employees, employees);
        }

        private List<Employee> GetEmployees()
        {
            return new List<Employee>
            {
                new(1234, ServiceType.P110),
            };
        }
    }
}