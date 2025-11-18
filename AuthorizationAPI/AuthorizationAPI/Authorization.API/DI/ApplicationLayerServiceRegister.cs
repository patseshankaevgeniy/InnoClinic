using Authorization.BLL.DI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Authorization.API.DI;

public static class ApplicationLayerServiceRegister
{
    public static IServiceCollection RegisterApplicationLayerDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterBusinessLayerDependencies(configuration);
        services.AddHttpContextAccessor();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Authorization API",
                Description = "An ASP.NET Core Web API",
            });
        });

        services
           .AddAuthorization()
           .AddAuthentication(options =>
           {
               options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
           })
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new()
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes(configuration["Auth:Key"]!)),
                   ValidateIssuer = true,
                   ValidIssuer = configuration["Auth:Issuer"],
                   ValidateAudience = true,
                   ValidAudience = configuration["Auth:Issuer"],
                   ValidateLifetime = true, 
                   ClockSkew = TimeSpan.FromMinutes(10)
               };
           });

        return services;
    }
}
