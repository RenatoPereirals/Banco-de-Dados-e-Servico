using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services;
using Moq;

namespace test.UnitTest.Domain.Services
{
    public class EmployeeServiceTest
    {
        private readonly Mock<IBsdRepository> _mockBsdRpository;
        private readonly EmployeeService _employeeService;

        public EmployeeServiceTest()
        {
            _mockBsdRpository = new Mock<IBsdRepository>();
            _employeeService = new EmployeeService(_mockBsdRpository.Object);
        }

        [Fact]
        public void Should_CalculateModulo11CheckDigit_For_ValidRegistration()
        {
            // Arrange
            var registration = 3782;

           // Act
            _employeeService.CalculateModulo11CheckDigit(registration);

            // Assert
            // Nada a fazer, se o método não lançar uma exceção, o teste passa
        }

        [Fact]
        public void Should_Return_Exception_For_InvalidRegistration()
        {
            // Arrange
            var invalidRegistration  = 123;

            // Act and Assert
            Assert.Throws<ArgumentException>(() => _employeeService.CalculateModulo11CheckDigit(invalidRegistration ));
        }

        [Fact]
        public void Should_Return_Exception_For_InvalidCharacter()
        {
            // Arrange
            var invalidRegistration  = -123;

            // Act and Assert
            Assert.Throws<ArgumentException>(() => _employeeService.CalculateModulo11CheckDigit(invalidRegistration ));
        }

        [Fact]
        public void Should_Return_Modulo11CheckDigit_For_Valid_Registration_Correctly()
        {
            // Arrange
            var registration = 3782;
            var expectedResul = 6;

            // Act
            var result =_employeeService.CalculateModulo11CheckDigit(registration);

            // Assert
            Assert.Equal(expectedResul, result);
        }
    }
}
