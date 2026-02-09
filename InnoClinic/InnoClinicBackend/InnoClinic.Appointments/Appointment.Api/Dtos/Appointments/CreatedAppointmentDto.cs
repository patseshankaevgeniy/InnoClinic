namespace Appointment.Api.Dtos.Appointments;

public record CreatedAppointmentDto
{
    public required Guid PatientId { get; init; }
    public required Guid DoctorId { get; init; }
    public required Guid OfficeId { get; init; }
    public required DateTime AppointmentDate { get; init; }
}
