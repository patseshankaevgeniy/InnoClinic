using Authorization.DAL.Entities;

namespace Authorization.BLL.Services.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(Identity user);
    string GenerateRefreshToken();
}
