using Bsd.Domain.Services.Interfaces;
using Bsd.Domain.Enums;
using Bsd.Domain.Entities;

using Bsd.Domain.Services;

namespace Bsd.Tests.Unit.Domain.Services;

public class RubricServiceTests
{
    private readonly RubricService _rubricService;
    private readonly Mock<IStaticDataService> _staticDataServiceMock;
    private readonly Mock<IDayTypeChecker> _dayTypeCheckerMock;

    public RubricServiceTests()
    {
        _staticDataServiceMock = new Mock<IStaticDataService>();
        _dayTypeCheckerMock = new Mock<IDayTypeChecker>();
        _rubricService = new RubricService(_staticDataServiceMock.Object, _dayTypeCheckerMock.Object);
    }

    [Fact]
    public async Task AssociateRubricsToEmployeeAsync_ThrowsException_WhenEmployeesIsNull()
    {
        // Arrange
        var bsdEntity = new BsdEntity();

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _rubricService.AssociateRubricsToEmployeeAsync(bsdEntity));
    }

    [Fact]
    public async Task AssociateRubricsToEmployeeAsync_ThrowsException_WhenNoWorkedDays()
    {
        // Arrange
        var bsdEntity = new BsdEntity
        {
            Employees = new List<Employee>
            {
                new()
            }
        };

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _rubricService.AssociateRubricsToEmployeeAsync(bsdEntity));
    }
}

