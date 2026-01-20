using Documents.DAL.Entities;

namespace Documents.DAL.Repositories.Interfaces;

public interface IDocumentRepository
{
    Task<Document?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Document>> GetByIdentityIdAsync(Guid identityId, CancellationToken cancellationToken);
    Task AddAsync(Document document, CancellationToken cancellationToken);
    void Delete(Document document);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
