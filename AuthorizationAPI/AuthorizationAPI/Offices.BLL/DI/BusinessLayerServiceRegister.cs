using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Offices.BLL.Services;
using Offices.BLL.Services.Interfaces;
using Offices.DAL.DI;
using System.Reflection;

namespace Offices.BLL.DI;

public static class BusinessLayerServiceRegister
{
    public static IServiceCollection RegisterBusinessLayerDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterDataLayerDependencies(configuration);
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IOfficesService, OfficesService>();
        services.AddScoped<IGuidService, GuidService>();

        return services;
    }
}
