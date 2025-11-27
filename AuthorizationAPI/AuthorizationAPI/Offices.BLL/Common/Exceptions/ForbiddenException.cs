namespace Offices.BLL.Common.Exceptions;

public sealed class ForbiddenException(string name) 
    : Exception($"Access to Entity \"{name}\" is forbidden.")
{
}
