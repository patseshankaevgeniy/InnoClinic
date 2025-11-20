namespace Authorization.BLL.Models;

public sealed class SignInModel
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
