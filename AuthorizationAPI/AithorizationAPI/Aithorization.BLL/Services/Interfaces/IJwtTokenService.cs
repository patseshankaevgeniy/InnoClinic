namespace Aithorization.BLL.Services.Interfaces;

public interface IJwtTokenService
{
    public string GenerateToken(Guid userId);
}
