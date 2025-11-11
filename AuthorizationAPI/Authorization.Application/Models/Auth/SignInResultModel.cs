namespace Authorization.Application.Models.Auth;

public class SignInResultModel
{
    public bool Succeeded { get; set; }
    public int? ErrorType { get; set; }
    public string Token { get; set; }
}
