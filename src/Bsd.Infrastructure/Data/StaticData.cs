using Bsd.Domain.Enums;
using Bsd.Domain.Entities;

namespace Bsd.Infrastructure.Data
{
    public static class StaticData
    {
        public static List<Employee> Employees { get; private set; }
        public static List<Rubric> Rubrics { get; private set; }

        static StaticData()
        {
            Employees = new List<Employee>
            {
                new() { EmployeeId = 1, ServiceType = ServiceType.P110 },
                new() { EmployeeId = 2, ServiceType = ServiceType.P140 },
                new() { EmployeeId = 3, ServiceType = ServiceType.P110 },
                // Adicionar mais funcionários
            };

            Rubrics = new List<Rubric>
            {
                new() { RubricId = 1902, Description = "P140", HoursPerDay = 2, DayType = DayType.Workday, ServiceType = ServiceType.P140 },
                new() { RubricId = 1913, Description = "P140", HoursPerDay = 2, DayType = DayType.Workday, ServiceType = ServiceType.P140 },
                new() { RubricId = 1935, Description = "P140", HoursPerDay = 2, DayType = DayType.Workday, ServiceType = ServiceType.P140 },
                new() { RubricId = 1937, Description = "P140", HoursPerDay = 2, DayType = DayType.Workday, ServiceType = ServiceType.P140 },
                // Adicionar mais rubricas
            };
        }
    }
}
