using Bsd.Domain.Entities;
using Bsd.Domain.Service;

namespace test.Domain.Services
{
    public class HoliDayCheckerTest
    {
        [Theory]
        [InlineData("1/1/2024")]
        [InlineData("8/12/2024")]
        [InlineData("25/12/2024")]
        public void Should_Return_true_For_Dates_That_Are_Holidays(string data)
        {
            // Arrange
            var holidayAdjusters = new VariableDateHolidayAdjuster();
            var holidayChecker = new HoliDayChecker(holidayAdjusters);
            var date = DateTime.Parse(data);

            // Act
            var result =  holidayChecker.IsHoliday(date);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("28/1/2025")]
        [InlineData("29/1/2024")]
        [InlineData("30/1/2034")]
        public void Should_Return_True_For_Dates_That_Are_PortuaryDay_Holidays(string data)
        {
            // Arrange
            var holidayAdjusters = new VariableDateHolidayAdjuster();
            var holidayChecker = new HoliDayChecker(holidayAdjusters);
            var date = DateTime.Parse(data);

            // Act
            var result =  holidayChecker.IsHoliday(date);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("2/1/2024")]
        [InlineData("30/1/2024")]
        [InlineData("10/6/2024")]
        public void Should_Return_False_For_Dates_That_Are_Not_Holidays(string data)
        {
            // Arrange
            var holidayAdjusters = new VariableDateHolidayAdjuster();
            var holidayChecker = new HoliDayChecker(holidayAdjusters);
            var date = DateTime.Parse(data);

            // Act
            var result =  holidayChecker.IsHoliday(date);

            // Assert
            Assert.False(result);
        }
    }
}