namespace Appointment.DAL.Entities;

public class AppointmentResultEntity : BaseEntity
{
    public required DateTime ResultDate { get; set; }
    public required DateTime AppointmentDate { get; set; }
    public required string Complaints { get; set; }
    public required string Conclusion { get; set; }
    public required string Recommendations { get; set; }
    public required Guid AppointmentId { get; set; }

    public required AppointmentEntity Appointment { get; set; }
}
