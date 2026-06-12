using Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(
            RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(
            HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                await HandleException(
                    context,
                    HttpStatusCode.NotFound,
                    ex.Message);
            }
            catch (ValidationException ex)
            {
                await HandleException(
                    context,
                    HttpStatusCode.BadRequest,
                    string.Join(", ", ex.Errors));
            }
            catch (Exception ex)
            {
                await HandleException(
                    context,
                    HttpStatusCode.InternalServerError,
                    ex.Message);
            }
        }

        private static async Task HandleException(
            HttpContext context,
            HttpStatusCode statusCode,
            string message)
        {
            context.Response.ContentType =
                "application/json";

            context.Response.StatusCode =
                (int)statusCode;

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(new
                {
                    error = message
                }));
        }
    }
}
