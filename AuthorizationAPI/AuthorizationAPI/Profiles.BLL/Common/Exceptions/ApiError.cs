using System.Net;

namespace Profiles.BLL.Common.Exceptions;

public class ApiError
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
}

public static class ExceptionHandlingExtensions
{
    public static ApiError GetErrorResult(this Exception ex)
    {
        var result = new ApiError();

        switch (ex)
        {
            case ValidationException validationException:
                result.StatusCode = (int)HttpStatusCode.BadRequest;
                result.Message = validationException.Message;
                break;
            case NotFoundException notFoundException:
                result.StatusCode = (int)HttpStatusCode.NotFound;
                result.Message = notFoundException.Message;
                break;
            case BadRequestException badRequestException:
                result.StatusCode = (int)HttpStatusCode.BadRequest;
                result.Message = badRequestException.Message;
                break;
            case AccessViolationException accessViolationException:
                result.StatusCode = (int)HttpStatusCode.Forbidden;
                result.Message = accessViolationException.Message;
                break;
            default:
                result.StatusCode = (int)HttpStatusCode.InternalServerError;
                result.Message = "Internal Server Error. " + ex.Message;
                break;
        }

        return result;
    }
}
