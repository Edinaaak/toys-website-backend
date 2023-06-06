using NBP_projekat.Exceptions;
using System.Text.Json;

namespace NBP_projekat.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> logger;
        private readonly RequestDelegate next;


        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                HandleException(context, ex);
            }
        }

        private async void HandleException(HttpContext httpContext, Exception ex)
        {
            var response = httpContext.Response;
            response.ContentType = "application/json";

            switch (ex)
            {
               
                case BadRequestException e:
                    response.StatusCode = 400;
                    break;
                case LoginCustomException e:
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                case MasterpieceCustomException e:
                    response.StatusCode = StatusCodes.Status408RequestTimeout;
                    break;
                default:
                    response.StatusCode = 500;
                    break;
            }


            var message = ex.Message;
            logger.LogError(message);
            var model = JsonSerializer.Serialize(new { msg = message });
            await response.WriteAsync(model);
        }
    }
        public static class ErrorHandlingMiddlewareExtension
        {

            public static void Register(this IApplicationBuilder app)
            {
                app.UseMiddleware<ErrorHandlingMiddleware>();
            }
        }
    
}
