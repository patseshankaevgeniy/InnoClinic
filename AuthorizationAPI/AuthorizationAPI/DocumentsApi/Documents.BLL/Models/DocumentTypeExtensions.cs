using Documents.DAL.Common.Enums;

namespace Documents.BLL.Models;

public static class DocumentTypeExtensions
{
    public static string GetMimeType(this DocumentType type) => type switch
    {
        DocumentType.Jpg => "image/jpeg",
        DocumentType.Pdf => "application/pdf",
        _ => "application/octet-stream"
    };
}
