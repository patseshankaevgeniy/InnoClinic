using Appointment.DAL.Repositories;
using Appointment.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Appointment.DAL.DI;

public static class DataLayerServiceRegister
{
    public static void RegisterDataLayerDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppointmentsDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IAppointmentsRepository, AppointmentsRepository>();
        services.AddScoped<IAppointmentResultsRepository, AppointmentResultsRepository>();
    }
}
