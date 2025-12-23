namespace Services.DAL.Entities;

public class Procedure : BaseEntity
{
    public decimal Price { get; set; }
    public Guid SpecializationId { get; set; }

    public Specialization Specialization { get; set; } = null!;
}
