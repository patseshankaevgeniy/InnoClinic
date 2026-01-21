using Authorization.BLL.DI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace Authorization.API.DI;

public static class ApplicationLayerServiceRegister
{
    public static IServiceCollection RegisterApplicationLayerDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterBusinessLayerDependencies(configuration);
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddHttpContextAccessor();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Authorization API",
                Description = "An ASP.NET Core Web API",
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                 {
                    new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                 }
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
