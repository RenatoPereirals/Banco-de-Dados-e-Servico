using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

using Bsd.API.Controllers;
using Bsd.Application.DTOs;
using Bsd.Application.Helpers.Interfaces;
using Bsd.Application.Interfaces;
using Bsd.Domain.Repository.Interfaces;
using Newtonsoft.Json;
using Bsd.API.ViewModels;

namespace test.API
{
    public class BsdControllerTests
    {
        private readonly Mock<IBsdRepository> _mockBsdRepository;
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
        private readonly Mock<IBsdApplicationService> _mockBsdApplication;
        private readonly Mock<IEmployeeValidationService> _mockEmployeeValidationService;
        private readonly BsdController _bsdController;
        private readonly CreateBsdRequest _request;

        public BsdControllerTests()
        {
            _mockBsdRepository = new Mock<IBsdRepository>();
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _mockBsdApplication = new Mock<IBsdApplicationService>();
            _mockEmployeeValidationService = new Mock<IEmployeeValidationService>();

            _bsdController = new BsdController(_mockBsdRepository.Object,
                                               _mockBsdApplication.Object,
                                               _mockEmployeeRepository.Object,
                                               _mockEmployeeValidationService.Object);

            _request = new CreateBsdRequest
            {
                BsdNumber = 1234,
                EmployeeRegistrations = new List<int> { 1234, 5678 },
                DateService = "01/01/2024",
                Digit = 8
            };
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtActionResult_WhenBSDIsSuccessfullyInserted()
        {
            // Arrange
            SetupEmployeeValidationService(true);
            SetupBsdApplicationService(_request, new CreateBsdRequest());

            // Act
            var result = await _bsdController.Post(_request);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(StatusCodes.Status201Created, createdAtActionResult.StatusCode);
        }

        [Fact]
        public async Task Post_ReturnsBadRequestObjectResult_WhenEmployeeNotFound()
        {
            // Arrange
            SetupEmployeeValidationService(false);

            // Act
            var result = await _bsdController.Post(_request);

            // Assert
            var expectedErrorMessage = "Funcionários com as matrículas 1234, 5678 não encontrados.";
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.Equal(expectedErrorMessage, badRequestResult.Value);
        }

        //[Fact]
        //public async Task Post_ReturnsInternalServerError_WhenExceptionIsThrown()
        //{
        //    // Arrange
        //    SetupEmployeeValidationService(true);
        //    SetupBsdApplicationService(_request, exception: new ApplicationException("Ocorreu um erro interno. Por favor, tente novamente."));

        //    // Act
        //    var result = await _bsdController.Post(_request);

        //    // Assert
        //    var statusCodeResult = Assert.IsType<ObjectResult>(result);
        //    Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);

        //    var actualErrorResponse = JsonConvert.DeserializeObject<ErrorResponseVm>(statusCodeResult.Value!.ToString()!);

        //    Assert.Equal(StatusCodes.Status500InternalServerError.ToString(), actualErrorResponse!.Errors.First().Logref);
        //    Assert.Equal("An internal server error has occurred.", actualErrorResponse.Errors.First().Message);
        //}

        private void SetupEmployeeValidationService(bool isValid)
        {
            _mockEmployeeValidationService
                .Setup(service => service.ValidateEmployeeRegistrationsAsync(It.IsAny<List<int>>()))
                .ReturnsAsync(isValid);
        }

        private void SetupBsdApplicationService(CreateBsdRequest request, CreateBsdRequest? response = null, Exception? exception = null)
        {
            if (exception != null)
            {
                _mockBsdApplication.Setup(app => app.CreateBsdAsync(request)).ThrowsAsync(exception);
            }
            else
            {
                _mockBsdApplication.Setup(app => app.CreateBsdAsync(request)).ReturnsAsync(response!);
            }
        }
    }
}
