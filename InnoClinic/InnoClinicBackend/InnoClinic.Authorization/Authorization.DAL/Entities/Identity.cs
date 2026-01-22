namespace Authorization.DAL.Entities;

public class Identity
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required string HashPassword { get; set; }
    public required UserRole Role { get; set; }
    public string? ExternalId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
