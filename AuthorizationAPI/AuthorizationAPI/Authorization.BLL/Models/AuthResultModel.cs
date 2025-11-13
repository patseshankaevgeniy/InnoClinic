namespace Authorization.BLL.Models;

public sealed class AuthResultModel
{
    public string AccessToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}
