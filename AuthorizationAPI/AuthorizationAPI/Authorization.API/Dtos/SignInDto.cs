namespace Authorization.API.Dtos;

public sealed class SignInDto
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}
