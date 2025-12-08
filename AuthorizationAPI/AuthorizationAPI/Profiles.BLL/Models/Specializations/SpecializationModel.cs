namespace Profiles.BLL.Models.Specializations;

public class SpecializationModel : BaseModel
{
    public required string Name { get; set; }
    public required List<string> Procedures { get; set; }
    public required List<string> Doctors { get; set; }
}
