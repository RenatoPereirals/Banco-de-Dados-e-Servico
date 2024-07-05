using Bsd.Domain.Entities;
using Bsd.Domain.Service;

namespace test.Domain.Services
{
    public class HoliDayCheckerTest
    {
        [Theory]
        [InlineData("28/1/2025")]
        [InlineData("29/1/2024")]
        [InlineData("30/1/2034")]
        public void IsVariableHoliday_ReturnsTrue_WhenIsPortuaryDay(string data)
        {
            // Arrange
            var holidayAdjuster = new VariableDateHolidayAdjuster();
            var holidayChecker = new HolidayChecker(holidayAdjuster);
            var date = DateTime.Parse(data);

            // Act
            var result = holidayChecker.IsHoliday(date);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("02/01/2024")]
        [InlineData("30/01/2024")]
        [InlineData("10/06/2024")]
        public void IsVariableHoliday_ReturnsFalse_ForNonHolidayDates(string dateString)
        {
            // Arrange
            var holidayAdjuster = new VariableDateHolidayAdjuster();
            var date = DateTime.ParseExact(dateString, "dd/MM/yyyy", null);

            // Act
            var result = holidayAdjuster.IsVariableHoliday(date);

            // Assert
            Assert.False(result);
        }

    }
}