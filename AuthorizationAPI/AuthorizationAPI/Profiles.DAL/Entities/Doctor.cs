using Profiles.DAL.Enums;

namespace Profiles.DAL.Entities;

public class Doctor : BaseEntity
{
    public required DoctorStatus Status { get; set; }
    public required DateTime CareerStartAt { get; set; }
    public required Guid SpecializationId { get; set; }

    public required Specialization Specialization { get; set; }
}
