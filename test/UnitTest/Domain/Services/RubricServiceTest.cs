using Bsd.Domain.Enums;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services;
using test.Domain.Services.TestDataBase;
using Bsd.Domain.Services.Interfaces;
using Moq;

namespace test.Domain.Services
{
    public class RubricServiceTest : TestBase
    {
        private readonly Mock<IBsdRepository> _mockBsdRepository;
        private readonly Mock<IRubricRepository> _mockRubricRepository;
        private readonly Mock<IEmployeeService> _mockEmployeeService;
        private readonly RubricService _rubricService;

        public RubricServiceTest()
        {
            _mockBsdRepository = new Mock<IBsdRepository>();
            _mockRubricRepository = new Mock<IRubricRepository>();
            _mockEmployeeService = new Mock<IEmployeeService>();
            _rubricService = new RubricService(_mockRubricRepository.Object, _mockBsdRepository.Object, _mockEmployeeService.Object);
        }

        [Fact]
        public async Task Should_Returns_Correct_Rubrics_By_ServiceType_And_DayType()
        {
            // Arrange
            var allRubrics = TestRubricsList;

            _mockRubricRepository.Setup(r => r.GetAllRubricsAsync()).ReturnsAsync(allRubrics);

            // Act
            var result = await _rubricService.FilterRubricsByServiceTypeAndDayAsync(ServiceType.P110, DayType.HoliDay);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, r =>
            {
                Assert.True(r.ServiceType == ServiceType.P110 && r.DayType == DayType.HoliDay);
            });
        }

        [Fact]
        public async Task Should_Rutrun_Lista_Total_Hours_Per_Month_By_Rubrics_Correctly()
        {
            // Arrange
            var startDate = new DateTime(2024, 1, 1);
            var endDate = new DateTime(2024, 12, 31);
            var testBsdEntitiesList = TestBsdList;

            _mockBsdRepository.Setup(r => r.GetEmployeeBsdEntitiesByDateRangeAsync(startDate, endDate))
                .ReturnsAsync(testBsdEntitiesList.SelectMany(eb => eb.EmployeeBsdEntities));

            _mockEmployeeService.Setup(s => s.CalculateEmployeeWorkedDays(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(daysWorked);

            var expectedRubricHours = TestEmployeeRubricHours;

            // Acta
            var result = await _rubricService.CalculateTotalHoursPerMonthByRubrics(startDate, endDate);

            // Assert
            Assert.Equal(expectedRubricHours.Count(), result.Count);
        }
    }
}
