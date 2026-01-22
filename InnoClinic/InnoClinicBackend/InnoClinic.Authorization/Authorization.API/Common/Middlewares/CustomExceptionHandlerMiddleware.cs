using Authorization.API.Dtos;
using Authorization.BLL.Common.Models.Exceptions;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Authorization.API.Common.Middlewares;

public sealed class CustomExceptionHandlerMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
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
                result = JsonConvert.SerializeObject(new ErrorDto { Message = validationException.Message });
                break;
            case BadRequestException badRequestException:
                code = HttpStatusCode.BadRequest;
                result = JsonConvert.SerializeObject(new ErrorDto { Message = badRequestException.Message });
                break;
            case ForbiddenException forbiddenException:
                code = HttpStatusCode.Forbidden;
                result = JsonConvert.SerializeObject(new ErrorDto { Message = forbiddenException.Message });
                break;
            case NotFoundException _:
                code = HttpStatusCode.NotFound;
                break;
            default:
                code = HttpStatusCode.InternalServerError;
                result = JsonConvert.SerializeObject(new ErrorDto { Message = exception.Message });
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        await context.Response.WriteAsync(result);
    }
}
