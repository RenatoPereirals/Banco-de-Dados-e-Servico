using Bsd.API.ViewModels;

using Bsd.Application.Utilities;

using Newtonsoft.Json;
using Serilog;
using System.Net;

namespace Bsd.API.Helpers
{
    public class ExceptionHandler
    {
        private static readonly bool IsDevelopmentOrQa = AppSettings.DevelopmentEnvironment == "Qa" || AppSettings.DevelopmentEnvironment == "Development";

        public static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            if (context.Response.HasStarted)
            {
                // Log a warning e retorne se a resposta já começou
                Log.Warning("A resposta já começou, não é possível modificar o status ou escrever no corpo.");
                return;
            }

            ErrorResponseVm errorResponseVm;
            HttpStatusCode statusCode;

            switch (ex)
            {
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    errorResponseVm = new ErrorResponseVm(statusCode.ToString(), notFoundException.Message);
                    break;
                case UnauthorizedAccessException unauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    errorResponseVm = new ErrorResponseVm(statusCode.ToString(), unauthorizedAccessException.Message);
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    if (IsDevelopmentOrQa)
                    {
                        errorResponseVm = new ErrorResponseVm(statusCode.ToString(),
                                                              $"{ex.Message} {ex?.InnerException?.Message}");
                    }
                    else
                    {
                        errorResponseVm = new ErrorResponseVm(statusCode.ToString(),
                                                              "An internal server error has occurred.");
                    }
                    break;
            }

            context.Response.Clear();
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var result = JsonConvert.SerializeObject(errorResponseVm);
            await context.Response.WriteAsync(result);
        }
    }

    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}