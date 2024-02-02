using Bsd.Domain.Entities;
using Bsd.Domain.Enums;
using Bsd.Domain.Services;

namespace test.UnitTest.Domain.Services
{
    public class EmployeeServiceTest
    {
        [Fact]
        public void Should_Validation_Registration_True()
        {
            // Arrange
            var registration = 3782;
            var serviceType = ServiceType.P140;
            var employee = new Employee(registration, serviceType);

            var employeeService = new EmployeeService(employee);

            // Act
            employeeService.ValidateRegistration(registration);

            // Assert

        }
    }
}
