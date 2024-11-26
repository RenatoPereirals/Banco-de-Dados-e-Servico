using Bsd.Application.Helpers;

using Bsd.Domain.Enums;
using Bsd.Domain.Services;
using Bsd.Domain.Services.Interfaces;
using Moq;

namespace test.UnitTest.Domain.Services;

public class DayTypeCheckerTest
{
    private readonly Mock<IHolidayChecker> _mockHolidayChecker;
    private readonly DayTypeChecker _dayTypeChecker;
    private readonly DateHelper _dateHelper;

    public DayTypeCheckerTest()
    {
        _mockHolidayChecker = new Mock<IHolidayChecker>();
        _dayTypeChecker = new DayTypeChecker(_mockHolidayChecker.Object);
        _dateHelper = new DateHelper();
    }

    #region WorkDay Tests

    [Theory]
    [InlineData("17/07/2024")]
    [InlineData("24/07/2024")]
    [InlineData("31/07/2024")]
    public void GetDayType_ReturnsWorkday_ForGivenWorkDay(string stringDate)
    {
        // Arrange
        var workDay = _dateHelper.ParseDate(stringDate);
        var expectedDayType = DayType.Workday;

        _mockHolidayChecker
            .Setup(hc => hc.IsHoliday(It.IsAny<DateTime>()))
            .Returns(false);

        // Act
        var actualDayType = _dayTypeChecker.GetDayType(workDay);

        // Assert
        Assert.Equal(expectedDayType, actualDayType);
    }

    #endregion

    #region Sunday Tests

    [Theory]
    [InlineData("07/01/2024")]
    [InlineData("14/01/2024")]
    [InlineData("21/01/2024")]
    public void GetDayType_ReturnsSunday_ForGivenDate(string stringDate)
    {
        // Arrange
        var sundayDate = _dateHelper.ParseDate(stringDate);
        var expectedDayType = DayType.Sunday;

        // Act
        var actualDayType = _dayTypeChecker.GetDayType(sundayDate);

        // Assert
        Assert.Equal(expectedDayType, actualDayType);
    }

    #endregion

    #region Holiday Tests
    [Fact]
    public void GetDayType_ReturnsHoliday_ForGivenHolidayDate()
    {
        // Arrange
        var holidayDate = _dateHelper.ParseDate("01/01/2024");
        var expectedDayType = DayType.Holiday;

        _mockHolidayChecker
            .Setup(hc => hc.IsHoliday(It.IsAny<DateTime>()))
            .Returns(true);

        // Act
        var actualDayType = _dayTypeChecker.GetDayType(holidayDate);

        // Assert
        Assert.Equal(expectedDayType, actualDayType);
    }

    #endregion

    #region HolidayAndSunday Tests

    [Fact]
    public void GetDayType_ReturnsSundayAndHoliday_ForGivenHolidayAndSundayDate()
    {
        // Arrange
        var holidayAndSundayDate = _dateHelper.ParseDate("21/04/2024");
        var expectedDayType = DayType.SundayAndHoliday;

        _mockHolidayChecker
            .Setup(hc => hc.IsHoliday(It.IsAny<DateTime>()))
            .Returns(true);

        // Act
        var actualDayType = _dayTypeChecker.GetDayType(holidayAndSundayDate);

        // Assert
        Assert.Equal(expectedDayType, actualDayType);
    }

    #endregion
}
