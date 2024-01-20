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
        private readonly BsdService _bsdService;
        private readonly List<Employee> _employees;
        private readonly List<int> _employeeRegistrations;

        public BsdServiceTests()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _mockRubricService = new Mock<IRubricService>();
            _bsdService = new BsdService(_mockEmployeeRepository.Object, _mockRubricService.Object);

            _employeeRegistrations = new List<int> { 1234, 2345, 3456 };
            _employees = TestEmployeeList;

            _mockEmployeeRepository
                .Setup(repo => repo.GetEmployeeByRegistrationAsync(It.IsAny<int>()))
                .Returns<int>(registration =>
                    Task.FromResult(_employees.FirstOrDefault(e => e.Registration == registration)
                )!);
        }

        [Fact]
        public async Task Should_Create_Bsd_Correctly()
        {
            // Arrange
            var dateService = DateTime.Parse("1/1/2024");
            var bsdNumber = 1234;

            // Act
            var result = await _bsdService.CreateBsdAsync(bsdNumber, dateService, _employeeRegistrations);

            // Assert
            Assert.Equal(bsdNumber, result.BsdNumber);
            Assert.Equal(dateService, result.DateService);
            Assert.Equal(_employeeRegistrations.Count, result.EmployeeBsdEntities.Count);
            foreach (var employeeBsdEntity in result.EmployeeBsdEntities)
            {
                Assert.Contains(employeeBsdEntity.Employee.Registration, _employeeRegistrations);
            }
        }

        [Fact]
        public async Task Should_Add_Employees_Correctly()
        {
            // Arrange
            var date = DateTime.Parse("1/1/2024");
            var bsdEntity = new BsdEntity(1234, date);

            // Act
            await _bsdService.AddEmployeesToBsdAsync(bsdEntity, _employeeRegistrations);

            // Assert
            Assert.Equal(_employeeRegistrations.Count, bsdEntity.EmployeeBsdEntities.Count);
            foreach (var employeeBsdEntity in bsdEntity.EmployeeBsdEntities)
            {
                Assert.Contains(employeeBsdEntity.Employee.Registration, _employeeRegistrations);
            }
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
