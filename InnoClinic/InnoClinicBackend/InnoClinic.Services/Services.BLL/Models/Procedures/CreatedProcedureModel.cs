namespace Services.BLL.Models.Procedures;

public class CreatedProcedureModel
{
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public required bool Status { get; set; }
    public required string SpecializationName { get; set; }
}
