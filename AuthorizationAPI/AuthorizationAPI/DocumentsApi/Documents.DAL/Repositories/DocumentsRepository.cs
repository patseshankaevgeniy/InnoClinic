using Documents.DAL.Entities;
using Documents.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Documents.DAL.Repositories;

public class DocumentRepository(ApplicationDbContext context) : IDocumentRepository
{
    public async Task<Document?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await context.Documents.AsNoTracking().FirstOrDefaultAsync(x=>x.Id == id, cancellationToken);

    public async Task<IEnumerable<Document>> GetByIdentityIdAsync(Guid identityId, CancellationToken cancellationToken) =>
        await context.Documents
                      .AsNoTracking()
                      .Where(d => d.IdentityId == identityId)
                      .ToListAsync(cancellationToken);

    public async Task AddAsync(Document document, CancellationToken cancellationToken) =>
        await context.Documents.AddAsync(document, cancellationToken);

    public void Delete(Document document) => context.Documents.Remove(document);

    public async Task SaveChangesAsync(CancellationToken cancellationToken) =>
        await context.SaveChangesAsync(cancellationToken);
}
