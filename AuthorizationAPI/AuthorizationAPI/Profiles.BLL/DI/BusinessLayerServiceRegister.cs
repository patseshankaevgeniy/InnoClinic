using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Profiles.BLL.Services;
using Profiles.BLL.Services.Interfaces;
using System.Reflection;

namespace Profiles.BLL.DI;

public static class BusinessLayerServiceRegister
{
    public static IServiceCollection RegisterBusinessLayerDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        // Register BLL services
        services.AddScoped<IDoctorsService, DoctorsService>();

        // Register AutoMapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
