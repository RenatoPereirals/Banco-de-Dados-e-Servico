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
            new("1", "a", 3M, DayType.HoliDay, ServiceType.P110),
            new("2", "b", 2M, DayType.HoliDay, ServiceType.P110),
            new("3", "c", 1M, DayType.Sunday, ServiceType.P140),
            new("4", "d", 1M, DayType.Workday, ServiceType.P140),
        };

        protected static List<Employee> TestEmployeeList => new()
        {
                new(1234, ServiceType.P110),
                new(2345, ServiceType.P140),
                new(3456, ServiceType.P140)
        };

        private static List<BsdEntity> GenerateTestBsdList()
        {
            var testBsdList = new List<BsdEntity>();
            var dateService = DateTime.Parse("1/1/2024");
            var employeeRegistration = 1234;

            for (int i = 0; i < daysWorked; i++)
            {
                var bsdEntity = new BsdEntity(employeeRegistration, dateService.AddDays(i * 2));
                var employee = new Employee(employeeRegistration, ServiceType.P140);
                var employeeBsdEntity = GenerateTestEmployeeBsdEntitiesList(employee, bsdEntity);

                bsdEntity.EmployeeBsdEntities.Add(employeeBsdEntity);

                testBsdList.Add(bsdEntity);
                // employeeRegistration += 1111;
            }

            return testBsdList;
        }

        private static EmployeeBsdEntity GenerateTestEmployeeBsdEntitiesList(Employee employee, BsdEntity bsdEntity)
        {
            var employeeBsdEntity = new EmployeeBsdEntity(employee.Registration, employee, bsdEntity.BsdNumber, bsdEntity)
            {
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