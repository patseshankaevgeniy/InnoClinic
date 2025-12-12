using Profiles.DAL.Models.Enums;

namespace Profiles.API.Dtos.Doctors;

public class DoctorDto: BaseDto
{
    public DoctorStatus Status { get; set; }
    public DateTime CareerStartAt { get; set; }
}
