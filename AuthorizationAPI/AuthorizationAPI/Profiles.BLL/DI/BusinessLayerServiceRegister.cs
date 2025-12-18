using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Profiles.BLL.Services;
using Profiles.BLL.Services.Interfaces;
using Profiles.DAL.DI;
using System.Reflection;

namespace Profiles.BLL.DI;

public static class BusinessLayerServiceRegister
{
    public static IServiceCollection RegisterBusinessLayerDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterDataLayerDependencies(configuration);
        
        // Register BLL services
        services.AddScoped<IDoctorsService, DoctorsService>();
        services.AddScoped<IPatientService, PatientService>();

        // Register AutoMapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());




        return services;
    }
}
