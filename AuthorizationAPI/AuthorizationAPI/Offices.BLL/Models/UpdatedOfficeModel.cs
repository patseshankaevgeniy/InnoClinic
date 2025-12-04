namespace Offices.BLL.Models;

public class UpdatedOfficeModel
{
    public Guid Id { get; set; }
    public required string Address { get; set; }
    public required string PhoneNumber { get; set; }
    public bool IsActive { get; set; }
}
