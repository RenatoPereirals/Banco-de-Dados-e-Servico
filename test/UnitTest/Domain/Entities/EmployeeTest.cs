using Bsd.Domain.Entities;
using Bsd.Domain.Enums;

namespace test.Domain.Entities
{
    public class EmployeeTest
    {
        [Fact]
        public void Employee_ShouldInitializePropertiesWithValues()
        {
            // Arrange
            var employee = new Employee();
            var expectedregistration = 1234;
            var expectedService = ServiceType.P140;
            var expectedEmployeeList = new List<EmployeeBsdEntity>();

            // Act
            employee.Registration = expectedregistration;
            employee.ServiceType = expectedService;
            employee.EmployeeBsdEntities = expectedEmployeeList;

            // Assert
            Assert.Equal(expectedregistration, employee.Registration);
            Assert.Equal(expectedService, employee.ServiceType);
            Assert.Same(expectedEmployeeList, employee.EmployeeBsdEntities);
        }
    }
}