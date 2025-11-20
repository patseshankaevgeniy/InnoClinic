using Authorization.DAL.Entities;

namespace Authorization.BLL.Models;

public sealed class IdentityModel
{
    public Guid? Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public UserRole Role { get; set; }
}
