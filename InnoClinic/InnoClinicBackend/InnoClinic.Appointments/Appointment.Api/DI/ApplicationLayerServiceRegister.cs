using Appointment.BLL.DI;
using System.Reflection;
using Microsoft.OpenApi.Models;

namespace Appointment.Api.DI;

public static class ApplicationLayerServiceRegister
{
    public static IServiceCollection RegisterApplicationLayerDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(TimeProvider.System)
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .RegisterBusinessLayerDependencies(configuration)
                .AddHttpContextAccessor();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Appointments API",
                Description = "An ASP.NET Core Web API",
            });
        });

        return services;
    }
}
