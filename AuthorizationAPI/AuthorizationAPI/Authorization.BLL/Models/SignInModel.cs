namespace Authorization.BLL.Models;

public sealed class SignInModel
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}
