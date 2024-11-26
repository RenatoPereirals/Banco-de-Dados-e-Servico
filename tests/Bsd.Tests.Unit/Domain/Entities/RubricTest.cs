using Bsd.Domain.Entities;
using Bsd.Domain.Enums;

namespace Bsd.Tests.Unit.Domain.Entities;

public class RubricTest
{
    [Fact]
    public void Rubric_Properties_ShouldBeSetCorrectly()
    {
        // Arrange
        var rubric = new Rubric();

        // Act
        rubric.RubricId = 1;
        rubric.DayType = DayType.Workday;
        rubric.ServiceType = ServiceType.P110;
        rubric.HoursPerDay = 8.0m;
        rubric.TotalWorkedHours = 40.0m;

        // Assert
        Assert.Equal(1, rubric.RubricId);
        Assert.Equal(DayType.Workday, rubric.DayType);
        Assert.Equal(ServiceType.P110, rubric.ServiceType);
        Assert.Equal(8.0m, rubric.HoursPerDay);
        Assert.Equal(40.0m, rubric.TotalWorkedHours);
    }
}