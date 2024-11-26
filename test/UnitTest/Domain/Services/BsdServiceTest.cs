using Bsd.Domain.Services.Interfaces;
using Bsd.Domain.Entities;

using Moq;
using Bsd.Domain.Services;

namespace test.UnitTest.Domain.Services;

public class BsdServiceTests
{
    [Fact]
    public async Task CreateOrUpdateBsdsAsync_AssociateRubricsToEmployeeAsyncCalled()
    {
        // Arrange
        var calculateRubricHoursMock = new Mock<ICalculateRubricHours>();
        var rubricServiceMock = new Mock<IRubricService>();
        var bsdService = new BsdService(calculateRubricHoursMock.Object, rubricServiceMock.Object);
        var bsd = new BsdEntity();

        // Act
        await bsdService.CreateOrUpdateBsdsAsync(bsd);

        // Assert
        rubricServiceMock.Verify(x => x.AssociateRubricsToEmployeeAsync(bsd), Times.Once);
    }

    [Fact]
    public async Task CreateOrUpdateBsdsAsync_CalculateTotalWorkedHoursCalled()
    {
        // Arrange
        var calculateRubricHoursMock = new Mock<ICalculateRubricHours>();
        var rubricServiceMock = new Mock<IRubricService>();
        var bsdService = new BsdService(calculateRubricHoursMock.Object, rubricServiceMock.Object);
        var bsd = new BsdEntity();

        // Act
        await bsdService.CreateOrUpdateBsdsAsync(bsd);

        // Assert
        calculateRubricHoursMock.Verify(x => x.CalculateTotalWorkedHours(bsd), Times.Once);
    }
}