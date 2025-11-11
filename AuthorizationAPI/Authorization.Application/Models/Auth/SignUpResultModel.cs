namespace Authorization.Application.Models.Auth;

public class SignUpResultModel
{
    public bool Succeeded { get; set; }
    public int? ErrorType { get; set; }
}
