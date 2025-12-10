using Profiles.BLL.Models.Specializations;
using Profiles.DAL.Models.Enums;

namespace Profiles.BLL.Models.Doctors;

public class UpdatedDoctorModel
{
    public required string PhoneNumber { get; set; }
    public DoctorStatus Status { get; set; }
    public SpecializationModel Specialization { get; set; }
}
