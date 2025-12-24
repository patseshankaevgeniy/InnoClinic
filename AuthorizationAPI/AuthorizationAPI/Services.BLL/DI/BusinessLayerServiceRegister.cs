using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.BLL.Services;
using Services.BLL.Services.Interfaces;
using Services.DAL.DI;
using System.Reflection;

namespace Services.BLL.DI;

public static class BusinessLayerServiceRegister
{
    public static IServiceCollection RegisterBusinessLayerDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterDataLayerDependencies(configuration);

        // Register BLL services
        services.AddScoped<ISpecializationsService, SpecializationsService>();
        services.AddScoped<IProceduresService, ProceduresService>();

        // Register AutoMapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
