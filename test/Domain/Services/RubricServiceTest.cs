using Bsd.Domain.Enums;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services;
using test.Domain.Services.TestDataBase;
using Moq;

namespace test.Domain.Services
{
    public class RubricServiceTest : TestBase
    {
        [Fact]
        public async Task GetRubricsByServiceTypeAndDayAsync_Returns_Correct_Rubrics()
        {
            // Arrange
            var mockRubricRepository = new Mock<IRubricRepository>();
            var rubricService = new RubricService(mockRubricRepository.Object);

            var allRubrics = TestRubrics;

            mockRubricRepository.Setup(r => r.GetAllRubricsAsync()).ReturnsAsync(allRubrics);

            // Act
            var result = await rubricService.GetRubricsByServiceTypeAndDayAsync(ServiceType.P110, DayType.HoliDay);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, r =>
            {
                Assert.True(r.ServiceType == ServiceType.P110 && r.DayType == DayType.HoliDay);
            });

        }

    }
}
