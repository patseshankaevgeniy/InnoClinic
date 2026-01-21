namespace Authorization.API.Dtos;

public sealed class SignUpDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string ReEnteredPassword { get; set; }
}
