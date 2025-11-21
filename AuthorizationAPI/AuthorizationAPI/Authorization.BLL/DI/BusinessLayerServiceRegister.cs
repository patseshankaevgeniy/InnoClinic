using Authorization.BLL.Services;
using Authorization.BLL.Services.Interfaces;
using Authorization.DAL.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Authorization.BLL.DI;

public static class BusinessLayerServiceRegister
{
    public static IServiceCollection RegisterBusinessLayerDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterDataLayerDependencies(configuration);
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IAuthService, AuthService>()
                .AddScoped<IJwtTokenService, JwtTokenService>()
                .AddScoped<IIdentityService, IdentityService>();

        return services;
    }
}
