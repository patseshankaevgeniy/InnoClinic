using Authorization.Api.Models;
using Authorization.Application.Models.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace Authorization.Api.Infrastructure;

public sealed class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandlerMiddleware(
        RequestDelegate next)
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
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;

        switch (exception)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonConvert.SerializeObject(new ErrorDto { Message = validationException.Message});
                break;
            case BadRequestException badRequestException:
                code = HttpStatusCode.BadRequest;
                result = JsonConvert.SerializeObject(new ErrorDto { Message = badRequestException.Message });
                break;
            case NotFoundException _:
                code = HttpStatusCode.NotFound;
                break;
            default:
                code = HttpStatusCode.InternalServerError;
                result = JsonConvert.SerializeObject(new ErrorDto { Message = exception.Message, Details = exception.StackTrace });
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        await context.Response.WriteAsync(result);
    }
}
