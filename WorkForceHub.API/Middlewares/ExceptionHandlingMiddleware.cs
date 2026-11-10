using FluentValidation;

namespace WorkForceHub.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        // Private Feilds
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly RequestDelegate _next;

        //Constructor
        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger, RequestDelegate next) {

            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context) {

            try {

                await _next(context);

            }
            catch (Exception ex) {

                _logger.LogError(ex, "An unexpected error Happened");

                context.Response.Clear();
                context.Response.ContentType = "application/json";

                switch (ex) {

                    case KeyNotFoundException:

                        context.Response.StatusCode = StatusCodes.Status404NotFound;

                        await context.Response.WriteAsJsonAsync(new
                        {
                            statusCode = StatusCodes.Status404NotFound,
                            message = ex.Message
                        });

                        break;

                    case ValidationException validationException:

                        context.Response.StatusCode = StatusCodes.Status400BadRequest;

                        await context.Response.WriteAsJsonAsync(new
                        {
                            statusCode = StatusCodes.Status400BadRequest,
                            message = "Validation Failed",
                            errors = validationException.Errors.Select(error => new
                            {
                                field = error.PropertyName,
                                message = error.ErrorMessage
                            })
                        });

                        break;

                    default:
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                        await context.Response.WriteAsJsonAsync(new
                        {

                            statusCode = StatusCodes.Status500InternalServerError,
                            message = "An unexpected srver error occurred..."
                        });
                        break;
                }


            }
        }
    }
}
