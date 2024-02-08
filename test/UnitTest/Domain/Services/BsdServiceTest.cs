using Bsd.Domain.Entities;
using Bsd.Domain.Enums;

using Bsd.Domain.Services;

using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services.Interfaces;

using test.Domain.Services.TestDataBase;

using Moq;

namespace test.Domain.Services.TestSetup
{
    public class BsdServiceTests : TestBase
    {
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
        private readonly Mock<IRubricService> _mockRubricService;
        private readonly Mock<IDayTypeChecker> _mockDayTypeChecer;
        private readonly BsdService _bsdService;
        private readonly List<Employee> _employees;

        public BsdServiceTests()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _mockRubricService = new Mock<IRubricService>();
            _mockDayTypeChecer = new Mock<IDayTypeChecker>();
            _bsdService = new BsdService(_mockEmployeeRepository.Object,
                                         _mockRubricService.Object,
                                         _mockDayTypeChecer.Object);
            _employees = TestEmployeeList;

            _mockEmployeeRepository
                .Setup(repo => repo.GetEmployeeByRegistrationAsync(It.IsAny<int>()))
                .Returns<int>(registration =>
                    Task.FromResult(_employees.FirstOrDefault(e => e.Registration == registration)
                )!);
        }

        [Fact]
        public async Task Should_Set_BsdNumber_Correctly()
        {
            // Arrange
            var dateService = DateTime.Parse("1/1/2024");
            var bsdNumber = 1234;
            var employeesRegistrations = new List<int> { 1234, 2345, 3456 };

            // Act
            var result = await _bsdService.CreateBsdAsync(bsdNumber, dateService, employeesRegistrations);

            // Assert
            Assert.Equal(bsdNumber, result.BsdNumber);
        }

        [Fact]
        public async Task Should_Set_DateService_Correctly()
        {
            // Arrange
            var dateService = DateTime.Parse("1/1/2024");
            var bsdNumber = 1234;
            var employeesRegistrations = new List<int> { 1234, 2345, 3456 };

            // Act
            var result = await _bsdService.CreateBsdAsync(bsdNumber, dateService, employeesRegistrations);

            // Assert
            Assert.Equal(dateService, result.DateService);
        }

        [Fact]
        public async Task Should_Add_Employees_To_Bsd_Correctly()
        {
            // Arrange
            var dateService = DateTime.Parse("1/1/2024");
            var bsdNumber = 1234;
            var employeesRegistrations = new List<int> { 1234, 2345, 3456 };

            // Act
            var result = await _bsdService.CreateBsdAsync(bsdNumber, dateService, employeesRegistrations);

            // Assert
            Assert.Equal(3, result.EmployeeBsdEntities.Count);
            foreach (var employeeBsdEntity in result.EmployeeBsdEntities)
            {
                Assert.Contains(employeeBsdEntity.Employee.Registration, employeesRegistrations);
            }
        }

        [Fact]
        public async Task Should_Add_Employees_Correctly()
        {
            // Arrange
            var date = DateTime.Parse("1/1/2024");
            var bsdEntity = new BsdEntity(1234, date);
            var employee = new Employee(2345, ServiceType.P140);
            var employeeBsdEntity = new EmployeeBsdEntity(employee.Registration, employee, bsdEntity.BsdNumber, bsdEntity);

            // Act
            await _bsdService.AddEmployeesToBsdAsync(bsdEntity, employee.Registration);

            // Assert
            var registrationExpected = employee.Registration;
            Assert.Contains(bsdEntity.EmployeeBsdEntities, bsd => bsd.EmployeeRegistration == registrationExpected);
        }


        [Fact]
        public async Task Should_Assign_Rubrics_Correctly()
        {
            // Arrange
            var allRubrics = TestRubricsList;
            var ListEmployee = TestEmployeeList;

            _mockEmployeeRepository.Setup(er => er.GetEmployeeByRegistrationAsync(It.IsAny<int>()))
                .Returns<int>(registration => Task.FromResult(
                    TestEmployeeList.FirstOrDefault(e => e.Registration == registration)
                )!);

            var dateService = DateTime.Parse("1/1/2024");

            var employeeBsdEntity = new EmployeeBsdEntity(
                1234,
                new Employee(1234, ServiceType.P140),
                54321,
                new BsdEntity(54321, dateService)
            );

            var dayType = DayType.HoliDay;
            var expectedRubrics = TestRubricsList;

            _mockRubricService
                .Setup(service => service.FilterRubricsByServiceTypeAndDayAsync(
                    It.IsAny<ServiceType>(), It.IsAny<DayType>()))
                        .ReturnsAsync(expectedRubrics);

            // Act
            await _bsdService.AssignRubricsToEmployeeByServiceTypeAndDayAsync(employeeBsdEntity, dayType);

            // Assert
            Assert.Equal(expectedRubrics, employeeBsdEntity.Rubrics);
        }
    }
}
