using Appointment.BLL.Services;
using Appointment.BLL.Services.Interfaces;
using Appointment.DAL.DI;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Appointment.BLL.DI;

public static class BusinessLayerServiceRegister
{
    public static IServiceCollection RegisterBusinessLayerDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterDataLayerDependencies(configuration);

        // Register BLL services
        services.AddScoped<IAppointmentsService, AppointmentsService>();

        services.AddMassTransit(x =>
        {

        });

        // Register AutoMapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
