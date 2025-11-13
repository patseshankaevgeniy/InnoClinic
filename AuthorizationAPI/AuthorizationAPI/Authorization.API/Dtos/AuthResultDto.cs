namespace Authorization.API.Dtos;

public sealed class AuthResultDto
{
    public required string? AccessToken { get; set; }
    public required string? RefreshToken { get; set; }
}
