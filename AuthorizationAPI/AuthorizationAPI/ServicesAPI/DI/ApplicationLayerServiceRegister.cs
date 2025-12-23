using Microsoft.OpenApi.Models;
using Services.BLL.DI;
using System.Reflection;

namespace Services.Api.DI;

public static class ApplicationLayerServiceRegister
{
    public static IServiceCollection RegisterApplicationLayerDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(TimeProvider.System);
        services.RegisterBusinessLayerDependencies(configuration);
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddHttpContextAccessor();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Services API",
                Description = "An ASP.NET Core Web API",
            });
        });

        return services;
    }
}
