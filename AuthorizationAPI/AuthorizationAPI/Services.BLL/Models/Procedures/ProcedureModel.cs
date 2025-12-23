namespace Services.BLL.Models.Procedures;

public class ProcedureModel : BaseModel
{
    public required string SpecializationName { get; set; }
    public required decimal Price { get; set; }
}
