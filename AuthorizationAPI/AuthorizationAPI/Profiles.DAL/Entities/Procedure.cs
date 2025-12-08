namespace Profiles.DAL.Entities;

public class Procedure
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required Guid SpecializationId { get; set; }

    public required Specialization Specialization { get; set; }
}
