namespace Offices.API.Dtos.Office;

public class CreatedOfficeDto
{
    public required string Address { get; set; }
    public required string PhoneNumber { get; set; }
    public bool IsActive { get; set; } = true;
}
