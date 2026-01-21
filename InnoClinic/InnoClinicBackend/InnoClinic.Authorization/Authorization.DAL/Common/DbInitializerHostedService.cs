using Authorization.DAL.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Authorization.DAL.Common;

public class DbInitializerHostedService(IServiceProvider serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

        for (int i = 0; i < 10; i++)
        {
            try
            {
                await context.Database.MigrateAsync(cancellationToken);
                await DbInitializer.SeedData(context, hasher);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Attempt {i + 1}] Database is not ready yet... Waiting.");
                await Task.Delay(5000, cancellationToken);
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
