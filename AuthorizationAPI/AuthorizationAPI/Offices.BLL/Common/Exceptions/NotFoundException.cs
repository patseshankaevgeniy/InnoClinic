namespace Offices.BLL.Common.Exceptions;

public sealed class NotFoundException : Exception
{
    public NotFoundException()
    {
    }

    public NotFoundException(string name)
       : base($"Entity \"{name}\" was not found.")
    {
    }
}
