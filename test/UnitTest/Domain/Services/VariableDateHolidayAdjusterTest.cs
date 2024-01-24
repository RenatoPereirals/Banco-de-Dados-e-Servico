using Bsd.Domain.Service;

namespace test.Domain.Services
{
    public class VariableDateHolidayAdjusterTest
    {
        private readonly VariableDateHolidayAdjuster _variableDateHolidayAdjuster;

        public VariableDateHolidayAdjusterTest()
        {
            _variableDateHolidayAdjuster = new VariableDateHolidayAdjuster();
        }

        [Theory]
        [InlineData("28/1/2025")]
        [InlineData("29/1/2024")]
        [InlineData("30/1/2034")]
        public void Should_Return_True_For_Variable_Date_Holiday(string data)
        {
            // Arrange
            var portuaryDay = DateTime.Parse(data);

            // Act
            var result = _variableDateHolidayAdjuster.IsVariableHoliday(portuaryDay);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Should_Return_False_For_Not_Variable_Date_Holiday()
        {
            // Arrange
            var portuaryDay = DateTime.Parse("28/1/2024");

            // Act
            var result = _variableDateHolidayAdjuster.IsVariableHoliday(portuaryDay);

            // Assert
            Assert.False(result);
        }
    }
}