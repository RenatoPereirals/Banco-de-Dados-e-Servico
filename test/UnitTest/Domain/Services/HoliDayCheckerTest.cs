using Bsd.Application.Helpers;
using Bsd.Domain.Entities;
using Bsd.Domain.Service;
using Bsd.Domain.Service.Interfaces;
using Moq;

namespace test.Domain.Services
{
    public class HoliDayCheckerTest
    {
        private readonly Mock<IVariableDateHolidayAdjuster> _variableDate;
        private readonly DateHelper _dateHelper;
        private readonly HolidayChecker _holidayChecker;

        public HoliDayCheckerTest()
        {
            _variableDate = new Mock<IVariableDateHolidayAdjuster>();
            _dateHelper = new DateHelper();
            _holidayChecker = new HolidayChecker(_variableDate.Object);
        }

        #region IsHoliday Test

        [Theory]
        [InlineData("01/01/2023")]
        [InlineData("06/03/2023")]
        [InlineData("07/04/2023")]
        [InlineData("21/04/2023")]
        [InlineData("01/05/2023")]
        [InlineData("24/06/2023")]
        [InlineData("16/07/2023")]
        [InlineData("07/09/2023")]
        [InlineData("12/10/2023")]
        [InlineData("02/11/2023")]
        [InlineData("15/11/2023")]
        [InlineData("08/12/2023")]
        [InlineData("25/12/2023")]
        public void IsVariableHoliday_ReturnsTrue_WhenIsHoliday(string dateString)
        {
            // Arrange
            var date = _dateHelper.ParseDate(dateString);

            // Act
            var result = _holidayChecker.IsHoliday(date);

            // Assert
            Assert.True(result);
        }

        #endregion

        #region IsHolidayEve Test

        [Theory]
        [InlineData("31/12/2023")]
        [InlineData("05/03/2023")]
        [InlineData("06/04/2023")]
        [InlineData("20/04/2023")]
        [InlineData("30/04/2023")]
        [InlineData("23/06/2023")]
        [InlineData("15/07/2023")]
        [InlineData("06/09/2023")]
        [InlineData("11/10/2023")]
        [InlineData("01/11/2023")]
        [InlineData("14/11/2023")]
        [InlineData("07/12/2023")]
        [InlineData("24/12/2023")]
        public void IsVariableHoliday_ReturnsTrue_WhenIsHolidayEve(string dateString)
        {
            // Arrange
            var date = _dateHelper.ParseDate(dateString);

            // Act
            var result = _holidayChecker.IsHolidayEve(date);

            // Assert
            Assert.True(result);
        }

        #endregion

        #region IsNotHooliday Test

        [Theory]
        [InlineData("02/01/2024")]
        [InlineData("30/01/2024")]
        [InlineData("10/06/2024")]
        public void IsVariableHoliday_ReturnsFalse_ForNonHolidayDates(string dateString)
        {
            // Arrange
            var date = _dateHelper.ParseDate(dateString);

            // Act
            var result = _holidayChecker.IsHoliday(date);

            // Assert
            Assert.False(result);
        }

        #endregion
    }
}