using Documents.BLL.Models;
using Documents.DAL.Common.Enums;
using System.Threading;

namespace Documents.BLL.Services.Interfaces;

public interface IDocumentService
{
    Task<DocumentModel> UploadAsync(Stream fileStream, string fileName, Guid identityId, DocumentType contentType, CancellationToken cancellationToken);

    Task<(byte[] Content, DocumentType ContentType)> GetDocumentAsync(Guid documentId, CancellationToken cancellationToken);

    Task DeleteAsync(Guid documentId, CancellationToken cancellationToken);

    Task<IEnumerable<DocumentModel>> GetByIdentityIdAsync(Guid identityId, CancellationToken cancellationToken);
}
