using Appointment.DAL.Entities;

namespace Appointment.Api.Dtos.Appointments;

public class UpdatedAppointmentDto
{
    public required DateTime AppointmentDate { get; set; }
    public AppointmentResultEntity? AppointmentResult { get; set; }
}
