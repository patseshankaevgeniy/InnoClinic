using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Profiles.DAL.Common;
using Profiles.DAL.Repositories;
using Profiles.DAL.Repositories.Interfaces;

namespace Profiles.DAL.DI;

public static class DataLayerServiceRegister
{
    public static void RegisterDataLayerDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProfilesDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("ProfilesDbConnection"));
        });

        services.AddMassTransit(x =>
        {
            x.AddEntityFrameworkOutbox<ProfilesDbContext>(o =>
            {
                o.UseSqlServer();
                o.UseBusOutbox();
            });

            x.UsingRabbitMq((context, cfg) =>
            {
                var rabbitHost = configuration["RabbitMq:Host"] ?? "localhost";

                cfg.Host(rabbitHost, "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddScoped<IDbTransactionManager, DbTransactionManager>();
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    }
}
