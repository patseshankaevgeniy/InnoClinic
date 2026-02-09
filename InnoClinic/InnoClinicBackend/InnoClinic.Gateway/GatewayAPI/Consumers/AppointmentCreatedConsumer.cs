using GatewayAPI.Hubs;
using InnoClinic.Contracts.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace GatewayAPI.Consumers;

public class AppointmentCreatedConsumer(IHubContext<NotificationHub> hubContext) : IConsumer<AppointmentCreatedEvent>
{
    private readonly IHubContext<NotificationHub> _hubContext = hubContext;

    public async Task Consume(ConsumeContext<AppointmentCreatedEvent> context)
    {
        var appointment = context.Message;

        await _hubContext.Clients.Group(appointment.OfficeId.ToString())
            .SendAsync("ReceiveNewAppointment", new
            {
                appointmentId = appointment.AppointmentId,
                officeId = appointment.OfficeId,
                dateTime = appointment.DateTime
            });
    }
}
