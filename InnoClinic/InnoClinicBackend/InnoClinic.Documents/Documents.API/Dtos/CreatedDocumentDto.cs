using Documents.DAL.Common.Enums;

namespace Documents.API.Dtos;

public class CreatedDocumentDto
{
    public required IFormFile File { get; set; }
    public required Guid IdentityId { get; set; }
    public required string FileName { get; set; }
    public required DocumentType ContentType { get; set; }

}
