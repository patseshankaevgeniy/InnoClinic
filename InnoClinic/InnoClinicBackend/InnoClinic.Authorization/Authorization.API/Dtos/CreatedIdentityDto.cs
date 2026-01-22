using Authorization.DAL.Entities;

namespace Authorization.API.Dtos;

public sealed class CreatedIdentityDto
{
    public Guid? Id { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required UserRole Role { get; set; }
}
