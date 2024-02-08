using Bsd.Domain.Enums;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services;
using test.Domain.Services.TestDataBase;
using Moq;

namespace test.Domain.Services
{
    public class RubricServiceTest : TestBase
    {
        private readonly Mock<IBsdRepository> _mockBsdRepository;
        private readonly Mock<IRubricRepository> _mockRubricRepository;
        private readonly RubricService _rubricService;

        public RubricServiceTest()
        {
            _mockBsdRepository = new Mock<IBsdRepository>();
            _mockRubricRepository = new Mock<IRubricRepository>();
            _rubricService = new RubricService(_mockRubricRepository.Object, _mockBsdRepository.Object);


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
        public async Task Should_Calculate_Total_Hours_Per_Month_By_Rubrics_Correct()
        {
            // Arrange
            var startDate = new DateTime(2023, 1, 1);
            var endDate = new DateTime(2023, 1, 31);
            var employeeBsdEntities = TestEmployeeBsdEntitiesList;

            _mockBsdRepository
                .Setup(r => r.GetEmployeeBsdEntitiesByDateRangeAsync(startDate, endDate))
                .ReturnsAsync(TestEmployeeBsdEntitiesList);

            // Act
            var result = await _rubricService.CalculateTotalHoursPerMonthByRubrics(startDate, endDate);

            // Assert

        }

    }
}
