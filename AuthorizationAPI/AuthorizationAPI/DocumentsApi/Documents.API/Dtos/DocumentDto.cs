using Documents.DAL.Common.Enums;

namespace Documents.API.Dtos;

public class DocumentDto
{
    public Guid Id { get; set; }
    public required Guid IdentityId { get; set; }
    public string FileName { get; set; } = null!;
    public DocumentType ContentType { get; set; }
    public string FileKey { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
