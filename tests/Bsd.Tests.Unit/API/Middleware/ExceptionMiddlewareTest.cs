using Bsd.API.Middlewares;

using Microsoft.AspNetCore.Http;

namespace Bsd.Tests.Unit.API.Middleware;

public class ExceptionMiddlewareTest
{
    private readonly Mock<RequestDelegate> _mockNext;
    private readonly Mock<HttpContext> _mockHttpContext;
    private readonly ExceptionMiddleware _exceptionMiddleware;

    public ExceptionMiddlewareTest()
    {
        _mockNext = new Mock<RequestDelegate>();
        _mockHttpContext = new Mock<HttpContext>();
        _exceptionMiddleware = new ExceptionMiddleware(_mockNext.Object);
    }

    [Fact]
    public async Task InvokeAsync_ShouldCallNextDelegate()
    {
        // Arrange
        _mockNext.Setup(next => next.Invoke(_mockHttpContext.Object)).Verifiable();

        // Act
        await _exceptionMiddleware.InvokeAsync(_mockHttpContext.Object);

        // Assert
        _mockNext.Verify();
    }
}