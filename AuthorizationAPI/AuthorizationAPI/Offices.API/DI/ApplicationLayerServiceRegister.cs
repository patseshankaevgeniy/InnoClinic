using Offices.BLL.DI;
using System.Reflection;

namespace Offices.API.DI;

public static class ApplicationLayerServiceRegister
{
    public static IServiceCollection RegisterApplicationLayerDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(TimeProvider.System);
        services.RegisterBusinessLayerDependencies(configuration);
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddHttpContextAccessor();

        return services;
    }
}
