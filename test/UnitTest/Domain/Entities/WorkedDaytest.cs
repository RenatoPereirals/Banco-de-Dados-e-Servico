namespace Bsd.Domain.Entities.Tests
{
    public class WorkedDayTests
    {
        [Fact]
        public void WorkedDay_Properties_SetCorrectly()
        {
            // Arrange
            var dateEntry = new DateTime(2022, 1, 1);
            var dateExit = new DateTime(2022, 1, 2);
            var startTime = new TimeOnly(9, 0);
            var endTime = new TimeOnly(17, 0);

            // Act
            var workedDay = new WorkedDay
            {
                DateEntry = dateEntry,
                DateExit = dateExit,
                StartTime = startTime,
                EndTime = endTime
            };

            // Assert
            Assert.Equal(dateEntry, workedDay.DateEntry);
            Assert.Equal(dateExit, workedDay.DateExit);
            Assert.Equal(startTime, workedDay.StartTime);
            Assert.Equal(endTime, workedDay.EndTime);
        }
    }
}