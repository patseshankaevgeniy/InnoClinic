using Profiles.DAL.Models.Enums;

namespace Profiles.API.Dtos.Doctors
{
    public class CreatedDoctorDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string MiddleName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string SpecializationName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DoctorStatus Status { get; set; }
        public DateTime CareerStartAt { get; set; }
    }
}
