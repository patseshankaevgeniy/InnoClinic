namespace Profiles.DAL.Entities;

public class Specialization : BaseEntity
{
    public required string Name { get; set; }

    public ICollection<Procedure> Procedures { get; set; } = new List<Procedure>();
    public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
