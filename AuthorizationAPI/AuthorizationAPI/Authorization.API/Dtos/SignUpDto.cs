namespace Aithorization.API.Dtos;

public sealed class SignUpDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ReEnteredPassword { get; set; }
}
