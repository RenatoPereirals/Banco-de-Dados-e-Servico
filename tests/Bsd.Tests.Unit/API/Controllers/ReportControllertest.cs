using Bsd.Application.Interfaces;
using Bsd.Application.DTOs;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Bsd.Domain.Services.Interfaces;
using Bsd.API.Controllers;

namespace Bsd.Tests.Unit.API.Controllers;

public class ReportControllerTests
{
    private readonly Mock<IReportService> _mockReportService;
    private readonly ReportController _reportController;

    public ReportControllerTests()
    {
        _mockReportService = new Mock<IReportService>();
        var mockStaticDataService = new Mock<IStaticDataService>();
        _reportController = new ReportController(_mockReportService.Object, mockStaticDataService.Object);
    }

    [Fact]
    public async Task GenerateReport_ReturnsOkResult_WhenReportGenerationIsSuccessful()
    {
        // Arrange
        var request = new ReportRequest();
        _mockReportService
            .Setup(rs => rs.ProcessReportAsync(request))
            .ReturnsAsync(true);

        // Act
        var result = await _reportController.GenerateReport(request);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        var okResult = (OkObjectResult)result;
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        Assert.Equal("Report generation initiated successfully.", okResult.Value);
    }

    [Fact]
    public async Task GenerateReport_ReturnsInternalServerError_WhenReportGenerationFails()
    {
        // Arrange
        var request = new ReportRequest();
        _mockReportService
            .Setup(rs => rs.ProcessReportAsync(request))
            .ReturnsAsync(false);

        // Act
        var result = await _reportController.GenerateReport(request);

        // Assert
        Assert.IsType<ObjectResult>(result);
        var objectResult = (ObjectResult)result;
        Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        Assert.Equal("Failed to generate the report.", objectResult.Value);
    }
}