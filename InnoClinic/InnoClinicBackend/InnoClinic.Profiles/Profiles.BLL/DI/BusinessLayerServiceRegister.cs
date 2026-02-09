using InnoClinic.Contracts.Grpc;
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

        services.AddGrpcClient<EntityChecker.EntityCheckerClient>("OfficeClient", o =>
        {
            o.Address = new Uri(configuration["ConnectionStrings:OfficesService"]!);
        });

        services.AddGrpcClient<EntityChecker.EntityCheckerClient>("SpecializationClient", o =>
        {
            o.Address = new Uri(configuration["ConnectionStrings:SpecializationsService"]!);
        });

        services.AddScoped<IDoctorsService, DoctorsService>();
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<IReceptionistsService, ReceptionistsService>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
