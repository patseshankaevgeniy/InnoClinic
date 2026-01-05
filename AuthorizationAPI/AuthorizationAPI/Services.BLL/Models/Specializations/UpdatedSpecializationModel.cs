using Services.BLL.Models.Procedures;

namespace Services.BLL.Models.Specializations;

public class UpdatedSpecializationModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required bool Status { get; set; }
    public List<ProcedureModel>? Procedures { get; set; }
}
