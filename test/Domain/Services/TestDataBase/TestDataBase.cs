using Bsd.Domain.Entities;
using Bsd.Domain.Enums;

namespace test.Domain.Services.TestDataBase
{
    public class TestBase
    {
        protected static List<Rubric> TestRubricsList => new()
        {
            new("1", "a", 3.0M, DayType.HoliDay, ServiceType.P110),
            new("2", "b", 2.0M, DayType.HoliDay, ServiceType.P110),
            new("3", "c", 1.0M, DayType.Sunday, ServiceType.P140),
            new("4", "d", 1.0M, DayType.Workday, ServiceType.P140),
        };

        protected static List<Employee> TestEmployeeList => new()
        {
                new(1234, ServiceType.P110),
                new(2345, ServiceType.P140),
                new(3456, ServiceType.P140)
        };

       protected static List<BsdEntity> TestBsdList => GenerateTestBsdList();

        private static List<BsdEntity> GenerateTestBsdList()
        {
            var testBsdList = new List<BsdEntity>();
            var startDate = DateTime.Parse("1/1/2024");
            var employeeRegistration = 1234;

            for (int i = 0; i < 7; i++)
            {
                var bsdEntity = new BsdEntity(employeeRegistration, startDate.AddDays(i * 2));
                var employee = new Employee(employeeRegistration, ServiceType.P140);
                var employeeBsdEntity = new EmployeeBsdEntity(employeeRegistration, employee, employeeRegistration, bsdEntity);
                bsdEntity.EmployeeBsdEntities.Add(employeeBsdEntity);

                testBsdList.Add(bsdEntity);
                employeeRegistration += 1111;
            }

            return testBsdList;
        }
    }
}