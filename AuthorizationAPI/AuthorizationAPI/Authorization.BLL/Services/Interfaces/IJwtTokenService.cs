namespace Aithorization.BLL.Services.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(Guid userId);
}
