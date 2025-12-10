using Profiles.DAL.Models.Enums;

namespace Profiles.DAL.Entities;

public class Doctor : User
{
    public required DoctorStatus Status { get; set; }
    public required DateTime CareerStartAt { get; set; }
    public Guid SpecializationId { get; set; }

    public Specialization Specialization { get; set; } = null!;
}
