namespace Services.BLL.Models.Procedures;

public class ProcedureModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required bool Status { get; set; }
    public required string SpecializationName { get; set; }
    public required decimal Price { get; set; }
}
