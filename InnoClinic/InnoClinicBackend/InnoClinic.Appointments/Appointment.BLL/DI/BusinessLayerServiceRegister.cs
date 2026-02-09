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

        services.AddScoped<IAppointmentsService, AppointmentsService>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
