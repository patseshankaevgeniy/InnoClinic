using Services.BLL.Models.Procedures;

namespace Services.BLL.Models.Specializations;

public class SpecializationModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required bool Status { get; set; }
    public List<ProcedureModel>? Procedures { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
