using Authorization.DAL.Common.Interfaces;
using Authorization.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Authorization.DAL.Common;

public static class DbInitializer
{
    public static async Task SeedData(
        ApplicationDbContext context, 
        IPasswordHasher passwordHasher,
        IConfiguration configuration)
    {
        await context.Database.MigrateAsync();

        if (await context.Identities.AnyAsync()) return;

        var admin = new Identity
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            Email = configuration["SeedData:DefaultAdminLogin"]!,
            HashPassword = passwordHasher.HashPassword(configuration["SeedData:DefaultAdminPassword"]!),
            Role = UserRole.Admin,
        };

        await context.Identities.AddAsync(admin);
        await context.SaveChangesAsync();
    }
}
