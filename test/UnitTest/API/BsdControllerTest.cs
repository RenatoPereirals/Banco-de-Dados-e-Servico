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

    public BsdControllerTests()
    {
        _mockBsdRepository = new Mock<IBsdRepository>();
        _mockEmployeeRepository = new Mock<IEmployeeRepository>();
        _mockBsdApplication = new Mock<IBsdApplicationService>();

        _bsdController = new BsdController(_mockBsdRepository.Object,
                                           _mockBsdApplication.Object,
                                           _mockEmployeeRepository.Object);


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
        var request = new CreateBsdRequest
        {
            BsdNumber = 1234,
            EmployeeRegistration = 1235,
            DateService = DateTime.UtcNow.ToString(),
            Digit = 8
        };

        var employee = new Employee { Registration = 1234 };

        _mockEmployeeRepository
            .Setup(repo => repo.GetEmployeeByRegistrationAsync(request.EmployeeRegistration))
            .ReturnsAsync(employee);

        _mockBsdApplication
            .Setup(app => app.CreateBsdAsync(request.BsdNumber, request.DateService, request.EmployeeRegistration, request.Digit))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _bsdController.Post(request);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(201, createdAtActionResult.StatusCode);
    }
}
