using Bsd.Domain.Services;

namespace test.Domain.Services.TestSetup
{
    public class BsdServiceTests
    {
        private readonly SetupBsdServiceTest _setup;
        private readonly BsdService _bsdService;

        public BsdServiceTests()
        {
            _setup = new SetupBsdServiceTest();
            _bsdService = new BsdService(_setup.EmployeeRepository.Object, _setup.RubricService.Object);
        }

        [Fact]
        public async void Should_Return_Correct_Rubrics_For_Each_Employee()
        {
            // Act
            var result = await _bsdService.FilterRubricsByServiceTypeAndDayAsync(_setup.Bsd);

            // Assert
            foreach (var employeeBsdEntity in _setup.Bsd.EmployeeBsdEntities)
            {
                var employeeId = employeeBsdEntity.Employee.Registration;
                var employeeServiceType = employeeBsdEntity.Employee.ServiceType;
                Assert.True(result.ContainsKey(employeeId));
                var rubrics = result[employeeId];
                Assert.All(rubrics, r =>
                {
                    Assert.Equal(employeeServiceType, r.ServiceType);
                    Assert.Equal(_setup.Bsd.DayType, r.DayType);
                });
            }
        }
    }
}