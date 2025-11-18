using Authorization.DAL.Entities;

namespace Authorization.API.Dtos;

public sealed class IdentityDto
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public UserRole Role { get; set; }
    public WorkerStatus? Status { get; set; }
}
