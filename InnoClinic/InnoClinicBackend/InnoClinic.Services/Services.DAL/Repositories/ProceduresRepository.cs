using Microsoft.EntityFrameworkCore;
using Services.DAL.Entities;
using Services.DAL.Repositories.Interfaces;

namespace Services.DAL.Repositories;

public class ProceduresRepository(ServicesDbContext db) : GenericRepository<Procedure>(db), IProceduresRepository
{
    private readonly DbSet<Procedure> _procedures = db.Set<Procedure>();

    public async new Task<Procedure?> GetByIdAsync(Guid id, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        return await GetQuery(asNoTracking)
            .Include(x => x.Specialization)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<List<Procedure>> GetBySpecializationIdAsync(Guid specializationId, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        return await GetQuery(asNoTracking)
            .Where(s => s.SpecializationId == specializationId)
            .ToListAsync(cancellationToken);
    }

    private IQueryable<Procedure> GetQuery(bool asNoTracking) =>
        asNoTracking ? _procedures.AsNoTracking() : _procedures;
}
