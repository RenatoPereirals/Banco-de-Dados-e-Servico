using Bsd.Domain.Entities;
using Bsd.Domain.Enums;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services;
using Moq;
using test.Domain.Services.TestDataBase;
using Xunit.Abstractions;

namespace test.UnitTest.Domain.Services
{
    public class BsdServiceTest : TestBase
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly Mock<IRubricRepository> _rubricRepositoryMock;
        private readonly BsdService _bsdService;
        private readonly ITestOutputHelper _output;

        public BsdServiceTest(ITestOutputHelper output)
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _rubricRepositoryMock = new Mock<IRubricRepository>();
            _bsdService = new BsdService(_employeeRepositoryMock.Object, _rubricRepositoryMock.Object);
            _output = output;
        }

        [Fact]
        public async Task AssociateRubricsToEmployeesAsync_ReturnsEmployeeRubrics_Wh()
        {
            // Arrange
            var bsdEntity = new BsdEntity
            {
                BsdId = 1,
                Employees = new List<Employee>
                {
                    new Employee { EmployeeId = 1234, ServiceType = ServiceType.P110 }
                }
            };

            var employee = bsdEntity.Employees.First();
            _output.WriteLine($"EmployeeId: {employee.EmployeeId}, ServiceType: {employee.ServiceType}");

            var rubrics = TestRubricsList.Where(
                rub => rub.ServiceType == employee.ServiceType && rub.DayType == DayType.Workday
            ).ToList();
            _output.WriteLine($"Rubrics count: {rubrics.Count}");

            _employeeRepositoryMock.Setup(repo => repo.GetEmployeeByRegistrationAsync(employee.EmployeeId))
                .ReturnsAsync(employee);

            _rubricRepositoryMock.Setup(repo => repo.GetRubricsByServiceTypeAndDayTypeAsync(
                employee.ServiceType, DayType.Workday
            )).ReturnsAsync(rubrics);

            // Act
            var result = await _bsdService.AssociateRubricsToEmployeesAsync(bsdEntity, DayType.Workday);

            // Assert
            _output.WriteLine($"Result count: {result.Count}");
            Assert.NotEmpty(result);
            Assert.All(result, er =>
            {
                Assert.Equal(bsdEntity.BsdId, er.BsdEntityId);
                Assert.Equal(employee.EmployeeId, er.EmployeeId);
                Assert.Contains(rubrics, r => r.RubricId == er.RubricId);
            });
        }
    }
}
