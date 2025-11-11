namespace Authorization.Application.Models.Exceptions;

public class AccessViolationException : Exception
{
    public AccessViolationException(string message) : base(message)
    {

    }
}
