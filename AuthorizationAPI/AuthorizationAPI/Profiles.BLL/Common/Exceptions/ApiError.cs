namespace Profiles.BLL.Common.Exceptions;

public class ApiError
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
}
