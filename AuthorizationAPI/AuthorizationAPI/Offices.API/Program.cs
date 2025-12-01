using Offices.API.DI;
using Offices.API.Middlewares;

namespace Offices.API;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.RegisterApplicationLayerDependencies(builder.Configuration);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddOpenApi();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseMiddleware<ExceptionHandlerMiddleware>();

        app.MapControllers();

        app.Run();
    }
}
