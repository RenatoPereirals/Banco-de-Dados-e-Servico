using Bsd.API.Helpers;
using Serilog;

namespace Bsd.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                LogException(ex);
                await ExceptionHandler.HandleExceptionAsync(context, ex);
            }
        }

        private static void LogException(Exception ex)
        {
            Log.Error(ex, "An unhandled exception has occurred.");
        }
    }
}
