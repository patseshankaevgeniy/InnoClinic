using Documents.DAL.Common.Enums;
using Documents.DAL.Common.Interfaces;

namespace Documents.DAL.Entities;

public class Document : IHasCreatedAt
{
    public Guid Id { get; init; }
    public required Guid IdentityId { get; set; }
    public required string FileName { get; set; }
    public required DocumentType ContentType { get; set; }
    public required string FileKey { get; set; }

    public DateTime CreatedAt { get; set; }
}
