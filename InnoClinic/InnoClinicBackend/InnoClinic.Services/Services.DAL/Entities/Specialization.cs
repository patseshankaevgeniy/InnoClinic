namespace Services.DAL.Entities;

public class Specialization : CatalogEntity
{
    public ICollection<Procedure> Procedures { get; set; } = new List<Procedure>();
}
