namespace Profiles.DAL.Entities;

public class Specialization 
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public ICollection<Procedure>? Procedures { get; set; }
    public ICollection<Doctor>? Doctors { get; set; }
}
