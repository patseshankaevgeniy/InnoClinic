using Authorization.BLL.Services.Interfaces;
using Authorization.BLL.Services;
using Authorization.DAL.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authorization.BLL.DI;

public static class BusinessLayerServiceRegister
{
    public static IServiceCollection RegisterBusinessLayerDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterDataLayerDependencies(configuration);
        services.AddScoped<IAuthService, AuthService>()
                .AddScoped<IJwtTokenService, JwtTokenService>()
                .AddScoped<IIdentityService, IdentityService>();

        return services;
    }
}
