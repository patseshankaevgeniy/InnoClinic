using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    }
}
