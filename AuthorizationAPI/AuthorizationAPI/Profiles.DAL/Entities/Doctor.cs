using Profiles.DAL.Enums;

namespace Profiles.DAL.Entities;

public class Doctor : BaseEntity
{
    public DoctorStatus Status { get; set; }
    public DateTime CareerStartAt { get; set; }
    public Guid SpecializationId { get; set; }

    public Specialization Specialization { get; set; }
}
