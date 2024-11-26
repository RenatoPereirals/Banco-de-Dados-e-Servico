using Bsd.Domain.Services.Interfaces;
using Bsd.Domain.Entities;
using Bsd.Domain.Services;

using Moq;

namespace test.UnitTest.Domain.Services;

public class CalculateRubricsHoursTest
{
    [Fact]
    public void CalculateTotalWorkedHours_ShouldUpdateRubrics()
    {
        // Arrange
        var bsdEntity = new BsdEntity
        {
            Employees = new List<Employee>
            {
                new() {
                    Rubrics = new List<Rubric>
                    {
                        new() { RubricId = 1, HoursPerDay = 8 },
                        new() { RubricId = 2, HoursPerDay = 6 },
                        new() { RubricId = 1, HoursPerDay = 4 },
                        new() { RubricId = 3, HoursPerDay = 7 }
                    }
                }
            }
        };

        var holidayChecker = new Mock<IHolidayChecker>().Object;
        var calculateRubricHours = new CalculateRubricHours(holidayChecker);

        // Act
        calculateRubricHours.CalculateTotalWorkedHours(bsdEntity);

        // Assert
        var employee = bsdEntity.Employees.First();
        Assert.Equal(12, employee.Rubrics.First(r => r.RubricId == 1).TotalWorkedHours);
        Assert.Equal(6, employee.Rubrics.First(r => r.RubricId == 2).TotalWorkedHours);
        Assert.Equal(7, employee.Rubrics.First(r => r.RubricId == 3).TotalWorkedHours);
    }
}

