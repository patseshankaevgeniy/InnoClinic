using Appointment.DAL.Entities;

namespace Appointment.Api.Dtos.Appointments;

public class AppointmentDto
{
    public Guid Id{ get; set; }
    public AppointmentResultEntity? AppointmentResult { get; set; }
    public required DateTime AppointmentDate { get; set; }
}
