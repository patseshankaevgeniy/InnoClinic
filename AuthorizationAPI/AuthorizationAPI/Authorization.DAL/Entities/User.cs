namespace Authorization.DAL.Entities;

public class User : BaseEntity
{
    public string Email { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public UserRole Role { get; set; } = default!;
    public string? Status { get; set; }
}
