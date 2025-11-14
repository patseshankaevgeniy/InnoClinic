using Authorization.BLL.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Authorization.BLL.Services;

public sealed class JwtTokenService(IConfiguration configuration) : IJwtTokenService
{
    public string GenerateToken(Guid userId)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        };

        var authenticationKey = configuration["Auth_Key"];
        if (string.IsNullOrEmpty(authenticationKey))
        {
            throw new InvalidOperationException("Authentication key is not configured.");
        }
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(
            issuer: configuration["Auth_Issuer"],
            audience: configuration["Auth_Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: credentials);

        var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        return token;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
