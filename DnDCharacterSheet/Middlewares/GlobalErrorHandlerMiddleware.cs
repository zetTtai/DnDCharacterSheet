
using DTOs;
using System.Net;
using System.Text.Json;

namespace Middlewares;

public class GlobalErrorHandlerMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Log the exception or perform any other action

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        return context.Response.WriteAsync(JsonSerializer.Serialize(
            new ErrorDTO(
                (int)HttpStatusCode.InternalServerError,
                "An unexpected error occurred:" + exception.Message
            )
        ));
    }
}
