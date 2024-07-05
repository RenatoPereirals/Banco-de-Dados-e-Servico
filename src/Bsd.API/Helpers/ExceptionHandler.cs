using Newtonsoft.Json;
using Bsd.API.ViewModels;
using System.Net;

namespace Bsd.API.Helpers
{
    public class ExceptionHandler
    {
        public static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
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
                    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" ||
                        Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Qa")
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

            context.Response.StatusCode = (int)statusCode;

            var result = JsonConvert.SerializeObject(errorResponseVm);
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(result);
        }
    }

    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}
