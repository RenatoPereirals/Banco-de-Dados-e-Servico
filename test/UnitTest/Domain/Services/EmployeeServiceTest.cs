using Bsd.Domain.Entities;
using Bsd.Domain.Enums;
using Bsd.Domain.Services;

namespace test.UnitTest.Domain.Services
{
    public class EmployeeServiceTest
    {
        [Fact]
        public void Should_Calculate_CheckDigit_Correctly()
        {
            // Arrange
            var registration = 3782;
            var serviceType = ServiceType.P140;
            var employee = new Employee(registration, serviceType);

            var employeeService = new EmployeeService(employee);

            // Act
            employeeService.SetRegistrationAndDigit(registration);

            // Assert
        }
    }
}
