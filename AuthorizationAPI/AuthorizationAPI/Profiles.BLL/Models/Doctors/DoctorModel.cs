using Profiles.DAL.Models.Enums;

namespace Profiles.BLL.Models.Doctors;

public sealed class DoctorModel : UserModel
{
    public Guid SpecializationId { get; set; }
    public required DoctorStatus Status { get; set; }
    public DateTime CareerStartAt { get; set; }
}
