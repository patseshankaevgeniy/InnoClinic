namespace Appointment.BLL.Models.Appointments;

public class CreatedAppointmentModel
{
    public required Guid PatientId { get; set; }
    public required Guid DoctorId { get; set; }
    public required Guid OfficeId { get; set; }
    public required DateTime AppointmentDate { get; set; }
}
