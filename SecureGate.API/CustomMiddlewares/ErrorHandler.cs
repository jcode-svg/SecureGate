using Newtonsoft.Json;
using SecureGate.Domain.ViewModels.Response;
using System.Net;
using static SecureGate.SharedKernel.AppConstants.ErrorMessages;

namespace SecureGate.API.CustomMiddlewares
{
    public class ErrorHandler
    {
        private readonly RequestDelegate _next;

        public ErrorHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                Console.WriteLine($"Error handler caught exception => {error.StackTrace}");

                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var responseBody = ResponseWrapper<string>.Error(ExceptionOccurred);

                await response.WriteAsync(JsonConvert.SerializeObject(responseBody));
            }
        }
    }
}
