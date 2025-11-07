namespace Authorization.Api.Models;

public class SignUpDto
{
    public string Name { get; set; }
    public string Password { get; set; }
    public string ReEnteredPassword { get; set; }
}
