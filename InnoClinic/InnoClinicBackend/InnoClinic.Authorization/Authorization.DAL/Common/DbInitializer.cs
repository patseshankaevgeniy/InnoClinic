using Authorization.DAL.Common.Interfaces;
using Authorization.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authorization.DAL.Common;

public static class DbInitializer
{
    public static async Task SeedData(ApplicationDbContext context, IPasswordHasher passwordHasher)
    {
        await context.Database.MigrateAsync();

        if (await context.Identities.AnyAsync()) return;

        var admin = new Identity
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            Email = "admin@innoclinic.com",
            HashPassword = passwordHasher.HashPassword("1234"),
            Role = UserRole.Admin,
        };

        await context.Identities.AddAsync(admin);
        await context.SaveChangesAsync();
    }
}
