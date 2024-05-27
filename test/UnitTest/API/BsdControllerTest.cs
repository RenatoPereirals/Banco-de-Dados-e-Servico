using Bsd.API.Controllers;
using Bsd.Application.DTOs;
using Bsd.Application.Interfaces;
using Bsd.Domain.Entities;
using Bsd.Domain.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace test.API;
public class BsdControllerTests
{
    private readonly Mock<IBsdRepository> _mockBsdRepository;
    private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
    private readonly Mock<IBsdApplicationService> _mockBsdApplication;
    private readonly BsdController _bsdController;
    private readonly CreateBsdRequest _request;

    public BsdControllerTests()
    {
        _mockBsdRepository = new Mock<IBsdRepository>();
        _mockEmployeeRepository = new Mock<IEmployeeRepository>();
        _mockBsdApplication = new Mock<IBsdApplicationService>();

        _bsdController = new BsdController(_mockBsdRepository.Object,
                                           _mockBsdApplication.Object,
                                           _mockEmployeeRepository.Object);

        _request = new CreateBsdRequest
        {
            BsdNumber = 1234,
            EmployeeRegistration = 1235,
            DateService = DateTime.UtcNow.ToString(),
            Digit = 8
        };
    }

    public override bool Equals(object? obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    [Fact]
    public async void Post_ReturnsCreatedAtActionResult_WhenBSDIsSuccessfullyInserted()
    {
        // Arrange
        var employee = new Employee { Registration = 1234 };

        _mockEmployeeRepository
            .Setup(repo => repo.GetEmployeeByRegistrationAsync(_request.EmployeeRegistration))
            .ReturnsAsync(employee);

        _mockBsdApplication
            .Setup(app => app.CreateBsdAsync(_request.BsdNumber, _request.DateService, _request.EmployeeRegistration, _request.Digit))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _bsdController.Post(_request);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(201, createdAtActionResult.StatusCode);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async void Post_ReturnsBadRequestObjectResult_WhenBSDIsInvalid(string dateService)
    {
        // Arrange
        _request.DateService = dateService;

        // Act
        var result = await _bsdController.Post(_request);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
        Assert.Equal("A data do serviço é obrigatória.", badRequestResult.Value);
    }
}
