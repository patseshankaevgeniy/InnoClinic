using Services.Api.Dtos.Procedures;

namespace Services.Api.Dtos.Specializations;

public class SpecializationDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required bool Status { get; set; }
    public List<ProcedureDto>? Procedures { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
