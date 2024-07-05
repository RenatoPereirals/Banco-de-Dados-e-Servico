using Bsd.API.ViewModels;

namespace test.UnitTest.API.ViewModels
{
    public class ErrorResponseVmTests
    {
        [Fact]
        public void Constructor_Default_ShouldInitializeCorrectly()
        {
            // Arrange & Act
            var errorResponse = new ErrorResponseVm();

            // Assert
            Assert.NotNull(errorResponse.TraceId);
            Assert.Empty(errorResponse.Errors);
        }

        [Fact]
        public void Constructor_WithLogrefAndMessage_ShouldInitializeCorrectly()
        {
            // Arrange
            string logref = "Logref1";
            string message = "Error message";

            // Act
            var errorResponse = new ErrorResponseVm(logref, message);

            // Assert
            Assert.NotNull(errorResponse.TraceId);
            Assert.Single(errorResponse.Errors);
            Assert.Equal(logref, errorResponse.Errors[0].Logref);
            Assert.Equal(message, errorResponse.Errors[0].Message);
        }

        [Fact]
        public void AddError_ShouldAddErrorCorrectly()
        {
            // Arrange
            var errorResponse = new ErrorResponseVm();
            string logref = "Logref1";
            string message = "Error message";

            // Act
            errorResponse.AddError(logref, message);

            // Assert
            Assert.Single(errorResponse.Errors);
            Assert.Equal(logref, errorResponse.Errors[0].Logref);
            Assert.Equal(message, errorResponse.Errors[0].Message);
        }
    }
}