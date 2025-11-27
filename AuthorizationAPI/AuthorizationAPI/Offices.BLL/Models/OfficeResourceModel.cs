namespace Offices.BLL.Models;

public class OfficeResourceModel
{
    public required string Address { get; set; }
    public required string PhoneNumber { get; set; }
    public bool IsActive { get; set; }
}
