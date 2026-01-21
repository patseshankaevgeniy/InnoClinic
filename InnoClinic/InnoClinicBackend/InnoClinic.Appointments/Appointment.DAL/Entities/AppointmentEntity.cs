namespace Appointment.DAL.Entities;

public class AppointmentEntity : BaseEntity
{
    public required DateTime AppointmentDate { get; set; }
    public AppointmentResultEntity? AppointmentResult { get; set; }
}
