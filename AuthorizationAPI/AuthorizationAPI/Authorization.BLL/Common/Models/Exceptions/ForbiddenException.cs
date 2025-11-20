namespace Authorization.BLL.Common.Models.Exceptions;

public sealed class ForbiddenException : Exception
{
    public ForbiddenException()
    {
    }

    public ForbiddenException(string name)
        : base($"Access to Entity \"{name}\" is forbidden.")
    {
    }

    public ForbiddenException(string name, Exception innerException)
        : base($"Access to Entity \"{name}\" ({innerException.InnerException}) is forbidden.")
    {
    }
}
