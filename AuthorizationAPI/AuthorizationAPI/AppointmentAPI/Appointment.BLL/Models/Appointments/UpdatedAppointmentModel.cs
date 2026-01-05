using Appointment.DAL.Entities;

namespace Appointment.BLL.Models.Appointments;

public class UpdatedAppointmentModel
{
    public Guid Id { get; set; }
    public required DateTime AppointmentDate { get; set; }
    public AppointmentResultEntity? AppointmentResult { get; set; }
}
