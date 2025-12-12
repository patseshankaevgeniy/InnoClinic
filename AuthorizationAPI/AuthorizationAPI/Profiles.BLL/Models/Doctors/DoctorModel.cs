using Profiles.BLL.Models.Specializations;
using Profiles.DAL.Models.Enums;

namespace Profiles.BLL.Models.Doctors;

public sealed class DoctorModel : UserModel
{
    public Guid SpecializationId { get; set; }
    public DoctorStatus Status { get; set; }
    public DateTime CareerStartAt { get; set; }

    public required SpecializationModel Specialization { get; set; }
}
