using Microsoft.AspNetCore.SignalR;

namespace GatewayAPI.Hubs;

public class NotificationHub : Hub
{
    public async Task JoinOfficeGroup(string officeId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, officeId);
    }

    public async Task LeaveOfficeGroup(string officeId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, officeId);
    }

    public override async Task OnConnectedAsync()
    {
        var officeId = Context.GetHttpContext()?.Request.Query["officeId"].ToString();

        if (!string.IsNullOrEmpty(officeId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, officeId);
        }
        else
        {
            Console.WriteLine($"[SignalR] Client {Context.ConnectionId} connected WITHOUT officeId");
        }

        await base.OnConnectedAsync();
    }
}
