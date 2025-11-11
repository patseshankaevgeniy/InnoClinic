using Authorization.Application.Models;

namespace Authorization.Application.Services.Interfaces;

public interface IJwtTokenService
{
    public string GenerateToken(JwtTokenGenerationOptions options);
}
