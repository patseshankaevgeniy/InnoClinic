using Profiles.BLL.Common.Exceptions;

namespace Profiles.API.Common.Middlewares;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var result = ex.GetErrorResult();

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = result.StatusCode;

            await context.Response.WriteAsJsonAsync(result);
        }
    }
}
