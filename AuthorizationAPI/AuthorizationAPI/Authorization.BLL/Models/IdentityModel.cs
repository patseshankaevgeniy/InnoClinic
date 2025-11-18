using Authorization.DAL.Entities;

namespace Authorization.BLL.Models;

public sealed class IdentityModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public UserRole Role { get; set; }
    public WorkerStatus? Status { get; set; }
}
