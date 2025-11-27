namespace Offices.BLL.Common.Exceptions;

public sealed class ForbiddenException : Exception
{
    public ForbiddenException(string name)
        : base($"Access to Entity \"{name}\" is forbidden.")
    {
    }
}
