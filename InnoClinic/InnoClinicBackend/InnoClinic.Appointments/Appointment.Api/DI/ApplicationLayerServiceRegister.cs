using Appointment.BLL.DI;
using MassTransit;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Appointment.Api.DI;

public static class ApplicationLayerServiceRegister
{
    public static IServiceCollection RegisterApplicationLayerDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(TimeProvider.System)
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .RegisterBusinessLayerDependencies(configuration)
                .AddHttpContextAccessor();

        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                var rabbitSettings = configuration.GetSection("RabbitMq");

                cfg.Host(rabbitSettings["Host"]!, "/", h =>
                {
                    h.Username(rabbitSettings["Username"]!);
                    h.Password(rabbitSettings["Password"]!);
                });

                cfg.ConfigureEndpoints(context);
            });
        });

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
