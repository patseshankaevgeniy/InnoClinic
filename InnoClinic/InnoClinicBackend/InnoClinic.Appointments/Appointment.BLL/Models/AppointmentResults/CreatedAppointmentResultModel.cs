namespace Appointment.BLL.Models.AppointmentResults;

public class CreatedAppointmentResultModel
{
    public required string Complaints { get; set; }
    public required string Conclusion { get; set; }
    public required string Recommendations { get; set; }
    public required Guid AppointmentId { get; set; }
}
