namespace Profiles.BLL.Common.Exceptions;

public class AccessViolationException : Exception
{
    public AccessViolationException(string message) : base(message)
    {
    }

    public AccessViolationException(string message, Exception exception)
        : base(message, exception)
    {
    }
}
