using Documents.BLL.Services;
using Documents.BLL.Services.Interfaces;
using Documents.DAL.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using System.Reflection;

namespace Documents.BLL.DI;

public static class BusinessLayerServiceRegister
{
    public static IServiceCollection RegisterBusinessLayerDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterDataLayerDependencies(configuration);
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<IDocumentService, DocumentService>();

        services.AddMinio(configureSource => configureSource
                .WithEndpoint(configuration["Minio:Endpoint"]) // localhost:9000
                .WithCredentials(configuration["Minio:AccessKey"], configuration["Minio:SecretKey"])
                .WithSSL(false)
                .Build());

        return services;
    }
}
