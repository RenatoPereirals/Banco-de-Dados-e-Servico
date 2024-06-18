using Bsd.Domain.Entities;
using Bsd.Domain.Enums;

namespace test.Domain.Services.TestDataBase
{
    public class TestBase
    {
        protected static int indexCount = 1;
        protected static int testEmployeeRegistration = 1234;
        protected static int daysWorked = 7;
        protected static IEnumerable<BsdEntity> TestBsdList => GenerateTestBsdList();
        // protected static IEnumerable<EmployeeRubricHours> TestEmployeeRubricHours => GenerateExpectedRubricHours();

        protected static List<Rubric> TestRubricsList => new()
        {
            // Codigo, Descrição, Horas/dia, Tipo de dia, Tipo de servico
            new()
            {
                RubricId = 1,
                Description = "a",
                HoursPerDay = 3M,
                DayType= DayType.HoliDay,
                ServiceType = ServiceType.P110
            },
            new()
            {
                RubricId = 1,
                Description = "a",
                HoursPerDay = 3M,
                DayType= DayType.HoliDay,
                ServiceType = ServiceType.P140
            },
            new()
            {
                RubricId =2,
                Description = "b",
                HoursPerDay = 2M,
                DayType = DayType.Workday,
                ServiceType = ServiceType.P110
            },
            new()
            {
                RubricId =2,
                Description = "b",
                HoursPerDay = 2M,
                DayType = DayType.Workday,
                ServiceType = ServiceType.P140
            },
            new()
            {
                RubricId =3,
                Description = "c",
                HoursPerDay = 1M,
                DayType = DayType.Sunday,
                ServiceType = ServiceType.P140
            },
            new()
            {
                RubricId =3,
                Description = "c",
                HoursPerDay = 1M,
                DayType = DayType.Sunday,
                ServiceType = ServiceType.P110
            },
            new()
            {
                RubricId = 4,
                Description = "d",
                HoursPerDay = 1M,
                DayType = DayType.SundayAndHoliday,
                ServiceType = ServiceType.P140
            },
            new()
            {
                RubricId = 4,
                Description = "d",
                HoursPerDay = 1M,
                DayType = DayType.SundayAndHoliday,
                ServiceType = ServiceType.P110
            }
        };

        protected static List<Employee> TestEmployeeList => new()
        {
                new()
                {
                    EmployeeId =1234,
                    ServiceType = ServiceType.P110
                },
                new()
                {
                    EmployeeId =2345,
                    ServiceType =ServiceType.P140
                },
                new()
                {
                    EmployeeId = 3456,
                    ServiceType = ServiceType.P140
                }
        };

        private static List<BsdEntity> GenerateTestBsdList()
        {
            var testBsdList = new List<BsdEntity>();
            var dateService = new DateTime(2024, 1, 1);
            var employeeRegistration = 1234;

            for (int i = 0; i < daysWorked; i++)
            {
                var bsdEntity = new BsdEntity()
                {
                    DateService = dateService.AddDays(i * 2)
                };
                var employee = new Employee()
                {
                    EmployeeId = employeeRegistration,
                    ServiceType = ServiceType.P140
                };

                bsdEntity.Employees.Add(employee);

                testBsdList.Add(bsdEntity);
            }

            return testBsdList;
        }
    }
}