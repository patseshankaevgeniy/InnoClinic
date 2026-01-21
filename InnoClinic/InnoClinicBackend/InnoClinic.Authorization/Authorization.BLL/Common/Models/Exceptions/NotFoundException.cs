namespace Authorization.BLL.Common.Models.Exceptions;

public sealed class NotFoundException : Exception
{
    public NotFoundException()
    {
    }

    public NotFoundException(string name)
       : base($"Entity \"{name}\" was not found.")
    {
    }

    public NotFoundException(string name, Exception innerException)
        : base($"Entity \"{name}\" by ({innerException.InnerException}) was not found.")
    {
    }
}
