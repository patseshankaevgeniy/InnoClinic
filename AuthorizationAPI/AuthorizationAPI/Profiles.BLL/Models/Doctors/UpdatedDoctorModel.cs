using Profiles.DAL.Models.Enums;

namespace Profiles.BLL.Models.Doctors;

public class UpdatedDoctorModel : UserModel
{
    public required DoctorStatus Status { get; set; }
    public required DateTime CareerStartAt { get; set; }
}
