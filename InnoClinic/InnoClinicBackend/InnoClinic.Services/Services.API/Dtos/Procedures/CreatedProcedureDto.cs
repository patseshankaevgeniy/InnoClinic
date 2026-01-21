namespace Services.Api.Dtos.Procedures;

public class CreatedProcedureDto
{
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public required bool Status { get; set; }
    public required string SpecializationName { get; set; }
}
