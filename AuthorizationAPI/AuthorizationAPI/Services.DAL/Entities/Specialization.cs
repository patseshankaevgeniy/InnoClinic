namespace Services.DAL.Entities;

public class Specialization : BaseEntity
{
  public ICollection<Procedure> Procedures { get; set; } = new List<Procedure>();
}
