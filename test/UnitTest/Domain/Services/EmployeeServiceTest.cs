using Bsd.Domain.Entities;
using Bsd.Domain.Enums;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services;
using Moq;

namespace test.UnitTest.Domain.Services
{
    public class EmployeeServiceTest
    {
        private readonly Mock<IBsdRepository> _mockBsdRpository;

        public EmployeeServiceTest()
        {
            _mockBsdRpository = new Mock<IBsdRepository>();
        }
        [Fact]
        public void Should_Validation_Registration_True()
        {
            // Arrange
            var registration = 3782;
            var serviceType = ServiceType.P140;
            var employee = new Employee(registration, serviceType);

            var employeeService = new EmployeeService(employee, _mockBsdRpository.Object);

            // Act
            employeeService.ValidateRegistration(registration);

            // Assert

        }
    }
}
