
using Bsd.API.Controllers;

using Bsd.Application.Interfaces;
using Bsd.Application.DTOs;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Moq;

namespace test.API.Controllers
{
    public class BsdControllerTests
    {
        private readonly Mock<IBsdApplicationService> _mockBsdApplication;
        private readonly BsdController _bsdController;
        private readonly CreateBsdRequest _request;

        public BsdControllerTests()
        {
            _mockBsdApplication = new Mock<IBsdApplicationService>();

            _bsdController = new BsdController(_mockBsdApplication.Object);

            _request = new CreateBsdRequest
            {
                BsdNumber = 1234,
                EmployeeId = 1234,
                DateService = "01/01/2024",
                Digit = 8
            };
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtActionResult_WhenBSDIsSuccessfullyInserted()
        {
            // Arrange
            SetupBsdApplicationService(_request, new CreateBsdRequest());

            // Act
            var result = await _bsdController.Post(_request);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(StatusCodes.Status201Created, createdAtActionResult.StatusCode);
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
