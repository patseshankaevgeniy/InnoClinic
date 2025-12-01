namespace Offices.API.Dtos;

public class OfficeResourceDto
{
    public required string Address { get; set; }
    public required string PhoneNumber { get; set; }
    public required bool IsActive { get; set; }
}
