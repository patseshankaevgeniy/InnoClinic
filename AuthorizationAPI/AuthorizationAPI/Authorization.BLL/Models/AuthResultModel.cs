namespace Authorization.BLL.Models;

public sealed class AuthResultModel
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}
