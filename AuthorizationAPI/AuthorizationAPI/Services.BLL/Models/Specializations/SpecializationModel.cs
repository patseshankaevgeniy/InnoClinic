using Services.BLL.Models.Procedures;

namespace Services.BLL.Models.Specializations;

public class SpecializationModel : BaseModel
{
    public List<ProcedureModel>? Procedures { get; set; }
}
