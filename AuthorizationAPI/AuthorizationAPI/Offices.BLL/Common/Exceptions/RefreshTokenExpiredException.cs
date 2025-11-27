namespace Offices.BLL.Common.Exceptions;

public sealed class RefreshTokenExpiredException : Exception
{
    public RefreshTokenExpiredException(string message)
        : base(message)
    {
    }
}
