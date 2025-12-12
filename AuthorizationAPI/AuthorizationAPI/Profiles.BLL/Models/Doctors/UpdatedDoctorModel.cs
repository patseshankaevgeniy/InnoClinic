using Profiles.DAL.Models.Enums;

namespace Profiles.BLL.Models.Doctors;

public class UpdatedDoctorModel : UserModel
{
    public DoctorStatus Status { get; set; }
    public required string SpecializationName { get; set; }
}
