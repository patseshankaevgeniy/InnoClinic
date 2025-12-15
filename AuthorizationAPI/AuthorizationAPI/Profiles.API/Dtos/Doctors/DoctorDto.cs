using Profiles.DAL.Models.Enums;

namespace Profiles.API.Dtos.Doctors;

public class DoctorDto: BaseDto
{
    public required DoctorStatus Status { get; set; }
    public required DateTime CareerStartAt { get; set; }
}
