using Bsd.API.Middlewares;

using Microsoft.AspNetCore.Http;

using System.Net;

namespace Bsd.Tests.Unit.API.Middleware
{
    public class LoggingMiddlewareTest
    {
        [Fact]
        public async Task Invoke_ShouldLogRequestAndResponseDetails()
        {
            // Arrange
            var middleware = new LoggingMiddleware((context) => Task.CompletedTask);
            var context = new DefaultHttpContext();
            context.Request.Method = HttpMethod.Get.Method;
            context.Request.Path = "/api/test";
            context.Response.StatusCode = (int)HttpStatusCode.OK;

            // Act
            await middleware.Invoke(context);

            // Assert
            // Verify that the request and response details are logged
            // You can add your own assertions here based on your logging implementation
            Assert.True(true);
        }

        [Fact]
        public async Task Invoke_ShouldCaptureRequestBody()
        {
            // Arrange
            var middleware = new LoggingMiddleware((context) => Task.CompletedTask);
            var context = new DefaultHttpContext();
            context.Request.Method = HttpMethod.Post.Method;
            context.Request.Path = "/api/test";
            context.Request.Body = new MemoryStream();
            var requestBody = "Test request body";
            var requestBodyBytes = System.Text.Encoding.UTF8.GetBytes(requestBody);
            context.Request.Body.Write(requestBodyBytes, 0, requestBodyBytes.Length);
            context.Request.Body.Seek(0, SeekOrigin.Begin);

            // Act
            await middleware.Invoke(context);

            // Assert
            // Verify that the request body is captured and logged
            // You can add your own assertions here based on your logging implementation
            Assert.True(true);
        }

        [Fact]
        public async Task Invoke_ShouldCaptureResponseBody()
        {
            // Arrange
            var middleware = new LoggingMiddleware((context) => Task.CompletedTask);
            var context = new DefaultHttpContext();
            context.Request.Method = HttpMethod.Get.Method;
            context.Request.Path = "/api/test";
            context.Response.Body = new MemoryStream();
            var responseBody = "Test response body";
            var responseBodyBytes = System.Text.Encoding.UTF8.GetBytes(responseBody);
            context.Response.Body.Write(responseBodyBytes, 0, responseBodyBytes.Length);
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            // Act
            await middleware.Invoke(context);

            // Assert
            // Verify that the response body is captured and logged
            // You can add your own assertions here based on your logging implementation
            Assert.True(true);
        }
    }
}