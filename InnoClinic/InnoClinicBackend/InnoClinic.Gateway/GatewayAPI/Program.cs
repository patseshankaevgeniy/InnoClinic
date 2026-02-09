using GatewayAPI.Consumers;
using GatewayAPI.Extensions;
using GatewayAPI.Hubs;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddAuthentication()
    .AddJwtBearer("Auth0Scheme", options =>
    {
        options.Authority = builder.Configuration["Auth0:Domain"];
        options.Audience = builder.Configuration["Auth0:Audience"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Auth0:Domain"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Auth0:Audience"],
            ValidateLifetime = true
        };

        options.Events = new JwtBearerEvents().AddSignalRSupport("/notificationHub");
    });

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<AppointmentCreatedConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"]!);
        cfg.ReceiveEndpoint("gateway-appointments-queue", e =>
        {
            e.ConfigureConsumer<AppointmentCreatedConsumer>(context);
        });
    });
});

builder.Services.AddSignalR();

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes("Auth0Scheme")
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("SignalRPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddOcelot(builder.Configuration)
                .AddPolly();

var app = builder.Build();

app.UseRouting();

app.UseCors("SignalRPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<NotificationHub>("/notificationHub");
});

await app.UseOcelot();

app.Run();
