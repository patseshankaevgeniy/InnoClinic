namespace Offices.BLL.Common.Exceptions;

public sealed class RefreshTokenExpiredException(string message) : Exception(message)
{
}
