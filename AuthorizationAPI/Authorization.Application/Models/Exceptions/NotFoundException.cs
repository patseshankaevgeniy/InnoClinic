namespace Authorization.Application.Models.Exceptions;

public sealed class NotFoundException : Exception
{
    public NotFoundException(string name, object key)
        : base($"Entity \"{name}\" by ({key}) was not found.")
    {
    }
}
