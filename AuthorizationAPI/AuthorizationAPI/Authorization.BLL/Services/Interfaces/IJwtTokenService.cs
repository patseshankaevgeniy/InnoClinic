namespace Authorization.BLL.Services.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(Guid userId);
    string GenerateRefreshToken();
}
