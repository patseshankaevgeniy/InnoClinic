namespace Offices.API.Dtos.Office;

public class UpdatedOfficeDto
{
    public required Guid Id { get; set; }
    public required string Address { get; set; }
    public required string PhoneNumber { get; set; }
    public required bool IsActive { get; set; }
}
