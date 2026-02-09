using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace GatewayAPI.Extensions;

public static class JwtBearerExtensions
{
    public static JwtBearerEvents AddSignalRSupport(this JwtBearerEvents events, string hubPath)
    {
        events.OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;

            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments(hubPath))
            {
                context.Token = accessToken;
            }

            return Task.CompletedTask;
        };

        return events;
    }
}
