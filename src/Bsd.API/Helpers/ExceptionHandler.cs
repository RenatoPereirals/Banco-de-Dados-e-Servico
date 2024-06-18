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

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" ||
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Qa")
            {
                errorResponseVm = new ErrorResponseVm(HttpStatusCode.InternalServerError.ToString(),
                                                      $"{ex.Message} {ex?.InnerException?.Message}");
            }
            else
            {
                errorResponseVm = new ErrorResponseVm(HttpStatusCode.InternalServerError.ToString(),
                                                      "An internal server error has occurred.");
            }

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = JsonConvert.SerializeObject(errorResponseVm);
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(result);
        }
    }
}
