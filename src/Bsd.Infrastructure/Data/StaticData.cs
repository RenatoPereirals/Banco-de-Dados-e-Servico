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
                // Funcionários da guarda Portuária P140
                new() { EmployeeId = 37702, ServiceType = ServiceType.P140 },
                new() { EmployeeId = 37710, ServiceType = ServiceType.P140 },
                new() { EmployeeId = 37826, ServiceType = ServiceType.P140 },
                new() { EmployeeId = 37940, ServiceType = ServiceType.P140 },
                new() { EmployeeId = 37958, ServiceType = ServiceType.P140 },
                new() { EmployeeId = 37990, ServiceType = ServiceType.P140 },
                new() { EmployeeId = 38067, ServiceType = ServiceType.P140 },
                new() { EmployeeId = 38083, ServiceType = ServiceType.P140 },
                new() { EmployeeId = 38164, ServiceType = ServiceType.P140 },
                // Funcionários de Operações portuárias P110
                new() { EmployeeId = 39411, ServiceType = ServiceType.P110 },
                new() { EmployeeId = 39438, ServiceType = ServiceType.P110 },
                new() { EmployeeId = 39586, ServiceType = ServiceType.P110 },
                new() { EmployeeId = 40932, ServiceType = ServiceType.P110 },
                new() { EmployeeId = 40940, ServiceType = ServiceType.P110 },
                new() { EmployeeId = 41076, ServiceType = ServiceType.P110 },
            };

            Rubrics = new List<Rubric>
            {
                new() { RubricId = 1902, HoursPerDay = 3, DayType = DayType.AllDays, ServiceType = ServiceType.P140 },
                new() { RubricId = 1913, HoursPerDay = 2, DayType = DayType.AllDays, ServiceType = ServiceType.P140 },
                new() { RubricId = 1921, HoursPerDay = 3, DayType = DayType.Holiday, ServiceType = ServiceType.P140 },
                new() { RubricId = 1931, HoursPerDay = 3, DayType = DayType.None, ServiceType = ServiceType.None },
                new() { RubricId = 1932, DayType = DayType.Holiday, ServiceType = ServiceType.P140 }, // 18 hours or 6 hours per day when is Holiday Eve
                new() { RubricId = 1934, HoursPerDay = 2, DayType = DayType.Holiday, ServiceType = ServiceType.P140 },
                new() { RubricId = 1935, DayType = DayType.AllDays, ServiceType = ServiceType.AllServices },
                new() { RubricId = 1937, HoursPerDay = 1, DayType = DayType.AllDays, ServiceType = ServiceType.P140 }, // Overtime starting from the 10th hour
                new() { RubricId = 1938, HoursPerDay = 1, DayType = DayType.Holiday, ServiceType = ServiceType.P140 },
            };
        }
    }
}
