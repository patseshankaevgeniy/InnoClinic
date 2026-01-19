namespace Services.Api.Dtos.Procedures;

public class ProcedureDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required bool Status { get; set; }
    public required decimal Price { get; set; }
    public required string SpecializationName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
