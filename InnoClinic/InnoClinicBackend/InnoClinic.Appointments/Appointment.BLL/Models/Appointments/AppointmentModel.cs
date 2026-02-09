using Appointment.DAL.Entities;
using Appointment.DAL.Enums;

namespace Appointment.BLL.Models.Appointments;

public class AppointmentModel
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid OfficeId { get; set; }
    public required AppointmentStatus Status { get; set; }
    public required DateTime AppointmentDate { get; set; }
    public AppointmentResultEntity? AppointmentResult { get; set; }
}
