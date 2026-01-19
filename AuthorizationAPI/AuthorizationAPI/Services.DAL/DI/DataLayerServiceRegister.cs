using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.DAL.Repositories;
using Services.DAL.Repositories.Interfaces;

namespace Services.DAL.DI;

public static class DataLayerServiceRegister
{
    public static void RegisterDataLayerDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ServicesDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("ServicesDbConnection"));
        });

        services.AddScoped<ISpecializationsRepository, SpecializationsRepository>()
                .AddScoped<IProceduresRepository, ProceduresRepository>();
    }
}
