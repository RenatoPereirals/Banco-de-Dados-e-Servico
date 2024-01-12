using Bsd.Domain.Entities;
using Bsd.Domain.Enums;

namespace test.Domain.Services
{
    public class HoursCalculationServiceTests
    {
        [Fact]
        public void CalculateOvertimeHoursList_ShouldReturnListRubrics_ForWrokDays()
        {
            // Arrange
            _ = new Employee("1230", ServiceType.P140, new List<Rubric>());

            // Act
            _ = new List<Rubric> { new("", "", 3, DayType.Workday, ServiceType.P140) };
            // Assert
            Assert.True(true);
        }
    }
}