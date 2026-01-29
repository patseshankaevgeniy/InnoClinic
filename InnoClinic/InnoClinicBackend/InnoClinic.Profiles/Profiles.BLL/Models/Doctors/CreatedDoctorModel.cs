namespace Profiles.BLL.Models.Doctors;

public sealed class CreatedDoctorModel : UserModel
{
    public required Guid OfficeId { get; set; }
    public required Guid SpecializationId { get; set; }
    public required DateTime CareerStartAt { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public Guid? PhotoId { get; set; }
}
