using Profiles.DAL.Models.Enums;

namespace Profiles.API.Dtos
{
    public class BaseDto
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string MiddleName { get; set; }
        public required string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
