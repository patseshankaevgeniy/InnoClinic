namespace Services.DAL.Entities;

public class CatalogEntity : BaseEntity
{
    public required string Name { get; set; }
    public bool Status { get; set; }
}
