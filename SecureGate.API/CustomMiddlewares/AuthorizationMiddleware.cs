using Newtonsoft.Json;
using SecureGate.Domain.ViewModels.Response;

namespace SecureGate.API.CustomMiddlewares
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized
                || context.Response.StatusCode == StatusCodes.Status403Forbidden)
            {
                //context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                var response = new UnauthorizedResponse();

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
