namespace Authorization.BLL.Models;

public sealed class SignUpModel
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string ReEnteredPassword { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}
