using Documents.DAL.Common.Enums;

namespace Documents.BLL.Models;

public class DocumentModel
{
    public Guid Id { get; set; }
    public Guid IdentityId { get; set; }
    public string FileName { get; set; } = null!;
    public DocumentType ContentType { get; set; }
    public string FileKey { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
