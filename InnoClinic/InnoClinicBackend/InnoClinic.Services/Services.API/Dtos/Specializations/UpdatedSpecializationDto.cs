namespace Services.Api.Dtos.Specializations;

public class UpdatedSpecializationDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required bool Status { get; set; }
}
