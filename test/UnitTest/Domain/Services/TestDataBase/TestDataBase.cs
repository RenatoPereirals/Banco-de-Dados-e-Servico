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
        protected static IEnumerable<EmployeeRubricHours> TestEmployeeRubricHours => GenerateExpectedRubricHours();

        protected static List<Rubric> TestRubricsList => new()
        {
            // Codigo, Descrição, Horas/dia, Tipo de dia, Tipo de servico
            new()
            {
                Code = "1",
                Description = "a",
                HoursPerDay = 3M,
                DayType= DayType.HoliDay,
                ServiceType = ServiceType.P110
            },
            new()
            {
                Code ="2",
                Description = "b",
                HoursPerDay = 2M,
                DayType = DayType.HoliDay,
                ServiceType = ServiceType.P110
            },
            new()
            {
                Code ="3",
                Description = "c",
                HoursPerDay = 1M,
                DayType = DayType.Sunday,
                ServiceType = ServiceType.P140
            },
            new()
            {
                Code ="4",
                Description = "d",
                HoursPerDay = 1M,
                DayType = DayType.Workday,
                ServiceType = ServiceType.P140
            }
        };

        protected static List<Employee> TestEmployeeList => new()
        {
                new()
                {
                    Registration =1234,
                    ServiceType = ServiceType.P110
                },
                new()
                {
                    Registration =2345,
                    ServiceType =ServiceType.P140
                },
                new()
                {
                    Registration = 3456,
                    ServiceType = ServiceType.P140
                }
        };

        private static List<BsdEntity> GenerateTestBsdList()
        {
            var testBsdList = new List<BsdEntity>();
            var dateService = DateTime.Parse("1/1/2024");
            var employeeRegistration = 1234;

            for (int i = 0; i < daysWorked; i++)
            {
                var bsdEntity = new BsdEntity()
                {
                    DateService = dateService.AddDays(i * 2)
                };
                var employee = new Employee()
                {
                    Registration = employeeRegistration,
                    ServiceType = ServiceType.P140
                };
                var employeeBsdEntity = GenerateTestEmployeeBsdEntitiesList(employee, bsdEntity);

                bsdEntity.EmployeeBsdEntities.ToList().Add(employeeBsdEntity);

                testBsdList.Add(bsdEntity);
                // employeeRegistration += 1111;
            }

            return testBsdList;
        }

        private static EmployeeBsdEntity GenerateTestEmployeeBsdEntitiesList(Employee employee, BsdEntity bsdEntity)
        {
            var employeeBsdEntity = new EmployeeBsdEntity()
            {
                EmployeeRegistration = employee.Registration,
                Employee = employee,
                BsdNumber = bsdEntity.BsdNumber,
                BsdEntity = bsdEntity,
                Rubrics = TestRubricsList
            };

            return employeeBsdEntity;
        }

        private static List<EmployeeRubricHours> GenerateExpectedRubricHours()
        {
            var list = new List<EmployeeRubricHours>();

            for (int i = 0; i < daysWorked; i++)
            {
                list.Add(new EmployeeRubricHours { EmployeeRegistration = testEmployeeRegistration, RubricCode = "1", TotalHours = 3M });
                list.Add(new EmployeeRubricHours { EmployeeRegistration = testEmployeeRegistration, RubricCode = "2", TotalHours = 2M });
                list.Add(new EmployeeRubricHours { EmployeeRegistration = testEmployeeRegistration, RubricCode = "3", TotalHours = 1M });
                list.Add(new EmployeeRubricHours { EmployeeRegistration = testEmployeeRegistration, RubricCode = "4", TotalHours = 1M });
            }

            return list;
        }
    }
}