using Documents.BLL.DI;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Documents.API.DI;

public static class ApplicationLayerServiceRegister
{
    public static IServiceCollection RegisterApplicationLayerDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterBusinessLayerDependencies(configuration);
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Documents API",
                Description = "An ASP.NET Core Web API",
            });
        });

        return services;
    }
}
