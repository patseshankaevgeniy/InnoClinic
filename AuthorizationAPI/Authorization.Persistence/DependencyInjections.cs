using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authorization.Persistence;

public static class DependencyInjections
{
    public static IServiceCollection AddPersistenceDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services
         .AddScoped(provider => provider.GetRequiredService<ApplicationDbContext>())
         .AddDbContext<ApplicationDbContext>(options =>
         {
             var connectionString = configuration.GetConnectionString("DefaultConnection");
             options.UseSqlServer(connectionString);
             options.EnableSensitiveDataLogging();
         });

        return services;
    }
}
