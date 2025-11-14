namespace Authorization.DAL.Entities;

public class User : BaseEntity
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = "";
}
