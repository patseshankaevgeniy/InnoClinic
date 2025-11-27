namespace Offices.BLL.Common.Exceptions;

public sealed class NotFoundException(string name)
    : Exception($"Entity \"{name}\" was not found.")
{
}
