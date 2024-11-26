using Bsd.Domain.Entities;

namespace Bsd.Tests.Unit.Domain.Entities;

public class EmployeeTest
{
    [Fact]
    public void Employee_Properties_ShouldBeSet()
    {
        // Arrange
        var employee = new Employee();

        // Act

        // Assert
        Assert.Equal(0, employee.EmployeeId);
        Assert.Empty(employee.WorkedDays);
        Assert.Empty(employee.Rubrics);
    }
}