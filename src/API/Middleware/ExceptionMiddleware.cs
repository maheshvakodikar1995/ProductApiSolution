using Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace API.Middleware;

/// <summary>
/// Catches unhandled exceptions and returns consistent JSON error responses.
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    /// <param name="logger">Logger for exception diagnostics.</param>
    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Invokes the middleware and handles domain and unexpected exceptions.
    /// </summary>
    /// <param name="context">The current HTTP context.</param>
    public async Task Invoke(
        HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex, "Resource not found: {Message}", ex.Message);

            await HandleException(
                context,
                HttpStatusCode.NotFound,
                ex.Message);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(
                "Validation failed: {Errors}",
                string.Join(", ", ex.Errors));

            await HandleException(
                context,
                HttpStatusCode.BadRequest,
                string.Join(", ", ex.Errors));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);

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
