using Authorization.DAL.Common;
using Authorization.DAL.Common.Interceptors;
using Authorization.DAL.Repositories;
using Authorization.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authorization.DAL.DI;

public static class DataLayerServiceRegister
{
    public static void RegisterDataLayerDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSingleton(TimeProvider.System)
            .AddSingleton<AuditInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            var interceptor = sp.GetRequiredService<AuditInterceptor>();

            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .AddInterceptors(interceptor);
        });

        services.AddHostedService<DbInitializerHostedService>();

        services.AddScoped<IIdentityRepository, IdentityRepository>();
    }
}
