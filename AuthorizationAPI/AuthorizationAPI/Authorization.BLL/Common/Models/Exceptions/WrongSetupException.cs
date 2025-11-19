namespace Authorization.BLL.Common.Models.Exceptions;

public sealed class WrongSetupException : Exception
{
    public WrongSetupException()
    {
    }

    public WrongSetupException(string message)
        : base(message)
    {
    }

    public WrongSetupException(string message, Exception exception)
        : base(message, exception)
    {
    }
}
