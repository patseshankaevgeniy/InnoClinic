using Authorization.DAL.Common.Interfaces;

namespace Authorization.BLL.Services;

public class PasswordHasher : IPasswordHasher
{
    private const int DefaultWorkFactor = 12;

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password, DefaultWorkFactor);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
    }
}
