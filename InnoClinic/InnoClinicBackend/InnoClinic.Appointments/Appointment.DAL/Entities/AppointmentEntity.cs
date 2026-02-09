using Appointment.DAL.Enums;

namespace Appointment.DAL.Entities;

public class AppointmentEntity : BaseEntity
{
    public required Guid PatientId { get; set; }
    public required Guid DoctorId { get; set; }
    public required Guid OfficeId { get; set; }
    public required AppointmentStatus Status { get; set; }
    public required DateTime AppointmentDate { get; set; }
    public AppointmentResultEntity? AppointmentResult { get; set; }
}
