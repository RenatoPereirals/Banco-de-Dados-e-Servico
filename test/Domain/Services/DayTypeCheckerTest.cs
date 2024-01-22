using Bsd.Domain.Enums;
using Bsd.Domain.Service.Interfaces;
using Bsd.Domain.Services;
using Moq;

namespace test.Domain.Services.TestDataBase
{
    public class DayTypeCheckerTest
    {
        private readonly Mock<IHoliDayChecker> _mockHolidayChecer;
        private readonly DayTypeChecker _dayTypeChecker;

        public DayTypeCheckerTest()
        {
            _mockHolidayChecer = new Mock<IHoliDayChecker>();
            _dayTypeChecker = new DayTypeChecker(_mockHolidayChecer.Object);
        }

        [Theory]
        [InlineData("7/1/2024")]
        [InlineData("14/1/2024")]
        [InlineData("21/1/2024")]
        public void Should_Return_DayType_For_Sunday_Correctly(string data)
        {
            // Arrange
            var sundayDate = DateTime.Parse(data);
            var dayTypeExpected = DayType.Sunday;

            // Act
            var dayTypeResult = _dayTypeChecker.GetDayType(sundayDate);

            //Assert
            Assert.Equal(dayTypeExpected, dayTypeResult);
        }

        [Fact]
        public void Should_Return_DayType_For_Holiday_Correctly()
        {
            // Arrange
            var HolidayDate = DateTime.Parse("1/1/2024");
            var dayTypeExpected = DayType.HoliDay;

            _mockHolidayChecer
                .Setup(hc => hc.IsHoliday(It.IsAny<DateTime>()))
                .Returns(true);

            // Act
            var dayTypeResult = _dayTypeChecker.GetDayType(HolidayDate);

            //Assert
            Assert.Equal(dayTypeExpected, dayTypeResult);
        }
    }
}