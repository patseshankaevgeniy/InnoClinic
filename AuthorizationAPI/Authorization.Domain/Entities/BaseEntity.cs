namespace Authorization.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CratedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
