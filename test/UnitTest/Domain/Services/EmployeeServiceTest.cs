using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services;
using Moq;
using test.Domain.Services.TestDataBase;

namespace test.UnitTest.Domain.Services
{
    public class EmployeeServiceTest : TestBase
    {
        private readonly Mock<IBsdRepository> _mockBsdRpository;
        private readonly EmployeeService _employeeService;

        public EmployeeServiceTest()
        {
            _mockBsdRpository = new Mock<IBsdRepository>();
            _employeeService = new EmployeeService(_mockBsdRpository.Object);
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

            // Act
            var result =_employeeService.CalculateModulo11CheckDigit(registration);

            // Assert
            var expectedDigit = 6;
            Assert.Equal(expectedDigit, result);
        }

        [Fact]
        public async Task Should_Calculate_Employee_Worked_Days_Correctly()
        {
            // Arrange
            var employeeRegistration = 1234;
            var startDate = new DateTime(2024, 1, 1);
            var endDate = new DateTime(2024, 12, 31);

            var testBsdEntitiesList = TestBsdList;

            _mockBsdRpository.Setup(r => r.GetAllBsdAsync()).ReturnsAsync(testBsdEntitiesList);

            // Act
            var workedDays = await _employeeService.CalculateEmployeeWorkedDays(employeeRegistration, startDate, endDate);

            // Assert
            var expectedWorkedDays = daysWorked; 
            Assert.Equal(expectedWorkedDays, workedDays);
        }

    }
}
