namespace Profiles.DAL.Entities;

public class Procedure : BaseEntity
{
    public required string Name { get; set; }
    public Guid SpecializationId { get; set; }

    public Specialization Specialization { get; set; } = null!;
}
