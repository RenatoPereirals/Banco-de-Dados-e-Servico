using Bsd.Domain.Service;

using System.Globalization;

namespace test.Domain.Services
{
    public class VariableDateHolidayAdjusterTest
    {
        private readonly VariableDateHolidayAdjuster _variableDateHolidayAdjuster;

        public VariableDateHolidayAdjusterTest()
        {
            _variableDateHolidayAdjuster = new VariableDateHolidayAdjuster();
        }
        private static DateTime ParseExactDate(string date)
        {
            var dateFormat = "dd/MM/yyyy";
            return DateTime.ParseExact(date, dateFormat, CultureInfo.InvariantCulture);
        }

        #region IsPortuaryDay Tests

        [Theory]
        [InlineData("28/1/2025")]
        [InlineData("29/1/2024")]
        [InlineData("30/1/2034")]
        public void IsVariableHoliday_ReturnsTrue_ForPortuaryDay(string date)
        {
            // Arrange
            var portuaryDay = ParseExactDate(date);

            // Act
            var result = _variableDateHolidayAdjuster.IsVariableHoliday(portuaryDay);

            // Assert
            Assert.True(result, $"{date} should be a Portuary Day.");
        }

        [Theory]
        [InlineData("28/1/2024")]
        [InlineData("15/8/2024")]
        [InlineData("10/3/2034")]
        public void IsVariableHoliday_ReturnsFalse_ForNonPortuaryDay(string date)
        {
            // Arrange
            var nonPortuaryDay = ParseExactDate(date);

            // Act
            var result = _variableDateHolidayAdjuster.IsVariableHoliday(nonPortuaryDay);

            // Assert
            Assert.False(result, $"{date} should not be a Portuary Day.");
        }

        #endregion

        #region IsEaster Tests

        [Theory]
        [InlineData("4/4/2021")]
        [InlineData("17/4/2022")]
        [InlineData("9/4/2023")]
        public void IsVariableHoliday_ReturnsTrue_ForEaster(string date)
        {
            // Arrange
            var easterDate = ParseExactDate(date);

            // Act
            var result = _variableDateHolidayAdjuster.IsVariableHoliday(easterDate);

            // Assert
            Assert.True(result, $"{date} should be Easter.");
        }

        [Theory]
        [InlineData("30/3/2025")]
        [InlineData("1/1/2026")]
        [InlineData("20/5/2027")]
        public void IsVariableHoliday_ReturnsFalse_ForNonEaster(string date)
        {
            // Arrange
            var nonEasterDate = ParseExactDate(date);

            // Act
            var result = _variableDateHolidayAdjuster.IsVariableHoliday(nonEasterDate);

            // Assert
            Assert.False(result, $"{date} should not be Easter.");
        }

        #endregion

        #region IsGoodFriday Tests

        [Theory]
        [InlineData("2/4/2021")]
        [InlineData("15/4/2022")]
        [InlineData("7/4/2023")]
        public void IsVariableHoliday_ReturnsTrue_ForGoodFriday(string date)
        {
            // Arrange
            var goodFridayDate = ParseExactDate(date);

            // Act
            var result = _variableDateHolidayAdjuster.IsVariableHoliday(goodFridayDate);

            // Assert
            Assert.True(result, $"{date} should be Good Friday.");
        }

        [Theory]
        [InlineData("30/4/2025")]
        [InlineData("1/1/2026")]
        [InlineData("20/5/2027")]
        public void IsVariableHoliday_ReturnsFalse_ForNonGoodFriday(string date)
        {
            // Arrange
            var nonGoodFridayDate = ParseExactDate(date);

            // Act
            var result = _variableDateHolidayAdjuster.IsVariableHoliday(nonGoodFridayDate);

            // Assert
            Assert.False(result, $"{date} should not be Good Friday.");
        }

        #endregion

        #region IsCorpusChristi Tests

        [Theory]
        [InlineData("3/6/2021")]
        [InlineData("16/6/2022")]
        [InlineData("8/6/2023")]
        public void IsVariableHoliday_ReturnsTrue_ForCorpusChristi(string date)
        {
            // Arrange
            var corpusChristiDate = ParseExactDate(date);

            // Act
            var result = _variableDateHolidayAdjuster.IsVariableHoliday(corpusChristiDate);

            // Assert
            Assert.True(result, $"{date} should be Corpus Christi.");
        }

        [Theory]
        [InlineData("30/4/2025")]
        [InlineData("1/1/2026")]
        [InlineData("20/5/2027")]
        public void IsVariableHoliday_ReturnsFalse_ForNonCorpusChristi(string date)
        {
            // Arrange
            var nonCorpusChristiDate = ParseExactDate(date);

            // Act
            var result = _variableDateHolidayAdjuster.IsVariableHoliday(nonCorpusChristiDate);

            // Assert
            Assert.False(result, $"{date} should not be Corpus Christi.");
        }

        #endregion
    }
}