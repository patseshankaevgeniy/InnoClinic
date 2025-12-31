using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddAuthentication()
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Auth:Key"]!)),

            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Auth:Issuer"],

            ValidateAudience = true,
            ValidAudience = builder.Configuration["Auth:Issuer"],

            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(10)
        };
    });

builder.Services.AddOcelot(builder.Configuration)
                .AddPolly();

var app = builder.Build();


app.UseAuthentication();
app.UseAuthorization();

await app.UseOcelot();

app.Run();
