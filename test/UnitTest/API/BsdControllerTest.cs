using Bsd.API.Controllers;
using Bsd.Application.DTOs;
using Bsd.Application.Interfaces;
using Bsd.Domain.Entities;
using Bsd.Domain.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
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
        var createdBsd = new CreateBsdRequest();

        _mockEmployeeRepository
            .Setup(repo => repo.GetEmployeeByRegistrationAsync(_request.EmployeeRegistration))
            .ReturnsAsync(employee);

        _mockBsdApplication
        .Setup(app => app.CreateBsdAsync(_request))
        .ReturnsAsync(createdBsd);

        // Act
        var result = await _bsdController.Post(_request);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(201, createdAtActionResult.StatusCode);
    }

    [Fact]
    public async void Post_ReturnsBadRequestObjectResult_WhenEmployeeNotFound()
    {
        // Arrange & Act
        var result = await _bsdController.Post(_request);

        // Assert
        var badRequestResult = Assert.IsAssignableFrom<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
        Assert.Equal("Funcionário com a matrícula 1235 não encontrado.", badRequestResult.Value);
    }

    [Fact]
    public async Task Post_ReturnsInternalServerError_WhenExceptionIsThrown()
    {
        // Arrange & Act
        var employee = new Employee { Registration = 1234 };

        _mockEmployeeRepository
            .Setup(repo => repo.GetEmployeeByRegistrationAsync(_request.EmployeeRegistration))
            .ReturnsAsync(employee);
        _mockBsdApplication.Setup(app => app.CreateBsdAsync(_request))
                            .ThrowsAsync(new ApplicationException(
                                "Ocorreu um erro interno. Por favor, tente novamente."));

        var result = await _bsdController.Post(_request);


        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        Assert.Equal("Ocorreu um erro interno. Por favor, tente novamente.", statusCodeResult.Value);
    }
}
