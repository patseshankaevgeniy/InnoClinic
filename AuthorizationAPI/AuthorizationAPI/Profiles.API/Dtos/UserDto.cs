using Profiles.DAL.Models.Enums;

namespace Profiles.API.Dtos;

public class UserDto
{
    public Guid? Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string MiddleName { get; set; }
    public required string PhoneNumber { get; set; }
    public required DateTime DateOfBirth { get; set; }
}
