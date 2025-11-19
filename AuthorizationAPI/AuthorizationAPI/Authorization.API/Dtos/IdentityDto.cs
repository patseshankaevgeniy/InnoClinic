using Authorization.DAL.Entities;

namespace Authorization.API.Dtos;

public sealed class IdentityDto
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string HashPassword { get; set; }
    public UserRole Role { get; set; }
}
