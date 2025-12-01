namespace Offices.API.Dtos;

public class OfficeInputDto
{
    public Guid? Id { get; set; }
    public required string Address { get; set; }
    public required string PhoneNumber { get; set; }
    public required bool IsActive { get; set; }
}
