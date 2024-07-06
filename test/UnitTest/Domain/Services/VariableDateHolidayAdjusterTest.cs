using Bsd.Application.Helpers;

using Bsd.Domain.Service;

using System.Globalization;

namespace test.Domain.Services
{
    public class VariableDateHolidayAdjusterTest
    {
        private readonly VariableDateHolidayAdjuster _variableDateHolidayAdjuster;
        private readonly DateHelper _dateHelper;

        public VariableDateHolidayAdjusterTest()
        {
            _variableDateHolidayAdjuster = new VariableDateHolidayAdjuster();
            _dateHelper = new DateHelper();
        }

        #region IsPortuaryDay Tests

        [Theory]
        [InlineData("28/01/2025")]
        [InlineData("29/01/2024")]
        [InlineData("30/01/2034")]
        public void IsVariableHoliday_ReturnsTrue_ForPortuaryDay(string dateString)
        {
            // Arrange
            var portuaryDay = _dateHelper.ParseDate(dateString);

            // Act
            var result = _variableDateHolidayAdjuster.IsVariableHoliday(portuaryDay);

            // Assert
            Assert.True(result, $"{dateString} should be a Portuary Day.");
        }

        [Theory]
        [InlineData("28/01/2024")]
        [InlineData("15/08/2024")]
        [InlineData("10/03/2034")]
        public void IsVariableHoliday_ReturnsFalse_ForNonPortuaryDay(string dateString)
        {
            // Arrange
            var nonPortuaryDay = _dateHelper.ParseDate(dateString);


            // Act
            var result = _variableDateHolidayAdjuster.IsVariableHoliday(nonPortuaryDay);

            // Assert
            Assert.False(result, $"{dateString} should not be a Portuary Day.");
        }

        #endregion

        #region IsEaster Tests

        [Theory]
        [InlineData("4/4/2021")]
        [InlineData("17/4/2022")]
        [InlineData("9/4/2023")]
        public void IsVariableHoliday_ReturnsTrue_ForEaster(string dateString)
        {
            // Arrange
            var easterDate = _dateHelper.ParseDate(dateString);


            // Act
            var result = _variableDateHolidayAdjuster.IsVariableHoliday(easterDate);

            // Assert
            Assert.True(result, $"{dateString} should be Easter.");
        }

        [Theory]
        [InlineData("30/03/2025")]
        [InlineData("01/01/2026")]
        [InlineData("20/05/2027")]
        public void IsVariableHoliday_ReturnsFalse_ForNonEaster(string dateString)
        {
            // Arrange
            var nonEasterDate = _dateHelper.ParseDate(dateString);


            // Act
            var result = _variableDateHolidayAdjuster.IsVariableHoliday(nonEasterDate);

            // Assert
            Assert.False(result, $"{dateString} should not be Easter.");
        }

        #endregion

        #region IsGoodFriday Tests

        [Theory]
        [InlineData("02/04/2021")]
        [InlineData("15/04/2022")]
        [InlineData("07/04/2023")]
        public void IsVariableHoliday_ReturnsTrue_ForGoodFriday(string dateString)
        {
            // Arrange
            var goodFridayDate = _dateHelper.ParseDate(dateString);


            // Act
            var result = _variableDateHolidayAdjuster.IsVariableHoliday(goodFridayDate);

            // Assert
            Assert.True(result, $"{dateString} should be Good Friday.");
        }

        [Theory]
        [InlineData("30/04/2025")]
        [InlineData("01/01/2026")]
        [InlineData("20/05/2027")]
        public void IsVariableHoliday_ReturnsFalse_ForNonGoodFriday(string dateString)
        {
            // Arrange
            var nonGoodFridayDate = _dateHelper.ParseDate(dateString);


            // Act
            var result = _variableDateHolidayAdjuster.IsVariableHoliday(nonGoodFridayDate);

            // Assert
            Assert.False(result, $"{dateString} should not be Good Friday.");
        }

        #endregion

        #region IsCorpusChristi Tests

        [Theory]
        [InlineData("03/06/2021")]
        [InlineData("16/06/2022")]
        [InlineData("08/06/2023")]
        public void IsVariableHoliday_ReturnsTrue_ForCorpusChristi(string dateString)
        {
            // Arrange
            var corpusChristiDate = _dateHelper.ParseDate(dateString);


            // Act
            var result = _variableDateHolidayAdjuster.IsVariableHoliday(corpusChristiDate);

            // Assert
            Assert.True(result, $"{dateString} should be Corpus Christi.");
        }

        [Theory]
        [InlineData("30/04/2025")]
        [InlineData("01/01/2026")]
        [InlineData("20/05/2027")]
        public void IsVariableHoliday_ReturnsFalse_ForNonCorpusChristi(string dateString)
        {
            // Arrange
            var nonCorpusChristiDate = _dateHelper.ParseDate(dateString);


            // Act
            var result = _variableDateHolidayAdjuster.IsVariableHoliday(nonCorpusChristiDate);

            // Assert
            Assert.False(result, $"{dateString} should not be Corpus Christi.");
        }

        #endregion
    }
}