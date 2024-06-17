using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace Bsd.API.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Logging request details
            Log.Information($"Request {context.Request.Method}: {context.Request.Path}");

            // Capture request body (optional)
            var requestBody = await CaptureRequestBody(context.Request);

            // Continue processing the request
            await _next(context);

            // Logging response details
            Log.Information($"Response {context.Response.StatusCode}: {context.Request.Method}: {context.Request.Path}");

            // Capture response body (optional)
            var responseBody = await CaptureResponseBody(context.Response);

            // Logging request and response bodies
            Log.Information($"Request Body: {requestBody}");
            Log.Information($"Response Body: {responseBody}");
        }

        private async Task<string> CaptureRequestBody(HttpRequest request)
        {
            request.EnableBuffering();

            var body = await new StreamReader(request.Body, Encoding.UTF8, true, 1024, true).ReadToEndAsync();
            request.Body.Position = 0;

            return body;
        }

        private async Task<string> CaptureResponseBody(HttpResponse response)
        {
            var originalBodyStream = response.Body;

            using (var responseBody = new MemoryStream())
            {
                response.Body = responseBody;

                await _next(response.HttpContext);

                response.Body.Seek(0, SeekOrigin.Begin);
                var body = await new StreamReader(response.Body).ReadToEndAsync();
                response.Body.Seek(0, SeekOrigin.Begin);

                return body;
            }
        }
    }
}