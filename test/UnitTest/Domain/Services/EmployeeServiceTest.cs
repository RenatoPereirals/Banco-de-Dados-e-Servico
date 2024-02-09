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
            var employeeService = new EmployeeService(_mockBsdRpository.Object);

           // Act
            employeeService.ValidateRegistration(registration);

            // Assert
            // Nada a fazer, se o método não lançar uma exceção, o teste passa
        }

        [Fact]
        public void Should_Validation_Registration_False()
        {
            // Arrange
            var registration = 123;
            var employeeService = new EmployeeService(_mockBsdRpository.Object);

            // Act and Assert
            Assert.Throws<ArgumentException>(() => employeeService.ValidateRegistration(registration));
        }
    }
}
