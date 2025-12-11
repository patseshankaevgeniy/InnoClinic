using Profiles.DAL.Models.Enums;

namespace Profiles.BLL.Models.Doctors;

public sealed class CreatedDoctorModel : UserModel
{
    public DoctorStatus Status { get; set; }
    public DateTime CareerStartAt { get; set; }

    public required string SpecializationName { get; set; }
}
