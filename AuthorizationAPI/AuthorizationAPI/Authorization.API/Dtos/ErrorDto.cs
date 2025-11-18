namespace Authorization.API.Dtos;

public sealed class ErrorDto
{
    public string Message { get; set; } = default!;
    public string? Details { get; set; }
}
