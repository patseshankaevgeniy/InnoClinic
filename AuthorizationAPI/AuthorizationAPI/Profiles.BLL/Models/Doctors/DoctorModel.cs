using Profiles.BLL.Models.Specializations;
using Profiles.DAL.Models.Enums;

namespace Profiles.BLL.Models.Doctors;

public class DoctorModel : BaseModel
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string MiddleName { get; set; }
    public required string PhoneNumber { get; set; }
    public DoctorStatus Status { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime CareerStartAt { get; set; }
    public SpecializationModel Specialization { get; set; }
}
