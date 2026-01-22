namespace Appointment.BLL.Models.AppointmentResults;

public class AppointmentResultModel
{
    public required Guid Id { get; set; }
    public required DateTime ResultDate { get; set; }
    public required DateTime AppointmentDate { get; set; }
    public required string Complaints { get; set; }
    public required string Conclusion { get; set; }
    public required string Recommendations { get; set; }
    public required Guid AppointmentId { get; set; }
}
