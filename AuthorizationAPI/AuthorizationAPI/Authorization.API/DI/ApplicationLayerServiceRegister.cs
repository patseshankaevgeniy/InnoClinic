using Authorization.BLL.DI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Authorization.API.DI;

public static class ApplicationLayerServiceRegister
{
    public static IServiceCollection RegisterApplicationLayerDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterBusinessLayerDependencies(configuration);

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "ToDo API",
                Description = "An ASP.NET Core Web API for managing ToDo items",
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
                   IssuerSigningKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes(configuration["Auth_Key"])),
                   ValidateIssuer = true,
                   ValidIssuer = configuration["Auth_Issuer"],
                   ValidateAudience = true,
                   ValidAudience = configuration["Auth_Issuer"],
                   ValidateLifetime = true, 
                   ClockSkew = TimeSpan.FromMinutes(2)
               };
           });

        return services;
    }
}
