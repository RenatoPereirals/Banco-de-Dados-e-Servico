using Bsd.API.Helpers;
using Bsd.API.ViewModels;

using Microsoft.AspNetCore.Http;

using System.Net;

using Newtonsoft.Json;

namespace Bsd.Tests.Unit.API.Helpers;

public class ExceptionHandlerTests
{
    [Theory]
    [InlineData("Production", "Test exception", "Test inner exception", "An internal server error has occurred.")]
    public async Task HandleExceptionAsync_ShouldReturnExpectedResponse(
    string environment,
    string exceptionMessage,
    string innerExceptionMessage,
    string expectedErrorMessage)
    {
        // Arrange
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", environment);

        var context = new DefaultHttpContext();
        var responseBodyStream = new MemoryStream();
        context.Response.Body = responseBodyStream;

        var exception = new Exception(exceptionMessage, new Exception(innerExceptionMessage));

        // Act
        await ExceptionHandler.HandleExceptionAsync(context, exception);

        // Assert
        Assert.Equal((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);
        Assert.Equal("application/json", context.Response.ContentType);

        responseBodyStream.Seek(0, SeekOrigin.Begin);
        var result = new StreamReader(responseBodyStream).ReadToEnd();

        var errorResponseVm = JsonConvert.DeserializeObject<ErrorResponseVm>(result);

        Assert.Equal(HttpStatusCode.InternalServerError.ToString(), errorResponseVm!.Errors.First().Logref);
        Assert.Equal(expectedErrorMessage, errorResponseVm.Errors.First().Message);
    }
}
