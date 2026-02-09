using Documents.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Minio;
using Minio.DataModel.Args;
using Testcontainers.Minio;
using Testcontainers.PostgreSql;

public class IntegrationTestFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private static readonly PostgreSqlContainer DbContainer = new PostgreSqlBuilder("postgres:15-alpine")
        .Build();

    private static readonly MinioContainer MinioContainer = new MinioBuilder("minio/minio")
        .WithUsername("minioadmin")
        .WithPassword("minioadmin")
        .WithEnvironment("MINIO_ROOT_USER", "minioadmin")
        .WithEnvironment("MINIO_ROOT_PASSWORD", "minioadmin")
        .Build();

    private static bool _isInitialized = false;
    private static readonly SemaphoreSlim _lock = new(1, 1);

    public async Task InitializeAsync()
    {
        await _lock.WaitAsync();
        try
        {
            if (_isInitialized) return;

            await Task.WhenAll(DbContainer.StartAsync(), MinioContainer.StartAsync());
            await CreateMinioBucketWithRetry(5);

            using var scope = Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await db.Database.MigrateAsync();

            _isInitialized = true;
        }
        finally
        {
            _lock.Release();
        }
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var minioEndpoint = new Uri(MinioContainer.GetConnectionString()).Authority;

        builder.UseSetting("ConnectionStrings:DefaultConnection", DbContainer.GetConnectionString());
        builder.UseSetting("Minio:Endpoint", minioEndpoint);

        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<DbContextOptions<ApplicationDbContext>>();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(DbContainer.GetConnectionString()));

            services.RemoveAll<IMinioClient>();
            services.AddMinio(config => config
                .WithEndpoint(minioEndpoint)
                .WithCredentials("minioadmin", "minioadmin")
                .WithSSL(false)
                .Build());
        });
    }

    private async Task CreateMinioBucketWithRetry(int retryCount)
    {
        var endpoint = new Uri(MinioContainer.GetConnectionString()).Authority;
        var client = new MinioClient()
            .WithEndpoint(endpoint)
            .WithCredentials("minioadmin", "minioadmin")
            .WithSSL(false)
            .Build();

        for (var i = 0; i < retryCount; i++)
        {
            try
            {
                var exists = await client.BucketExistsAsync(new BucketExistsArgs().WithBucket("documents"));
                if (!exists)
                {
                    await client.MakeBucketAsync(new MakeBucketArgs().WithBucket("documents"));
                }
                return;
            }
            catch (Exception) when (i < retryCount - 1)
            {
                await Task.Delay(1000);
            }
        }
    }

    public new Task DisposeAsync() => Task.CompletedTask;
}
