using Authorization.DAL.Entities;

namespace Authorization.BLL.Services.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(User user);
    string GenerateRefreshToken();
}
