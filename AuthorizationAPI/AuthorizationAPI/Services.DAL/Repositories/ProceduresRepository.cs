using Microsoft.EntityFrameworkCore;
using Services.DAL.Entities;
using Services.DAL.Repositories.Interfaces;

namespace Services.DAL.Repositories;

public class ProceduresRepository(ServicesDbContext db) : GenericRepository<Procedure>(db), IProceduresRepository
{
    private readonly DbSet<Procedure> _procedures = db.Set<Procedure>();

    public async Task<Procedure?> FindAsync(string procedureName, bool asNoTracking = default, CancellationToken cancellationToken = default)
    {
        return await GetQuery(asNoTracking)
            .FirstOrDefaultAsync(s => s.Name == procedureName, cancellationToken);
    }

    public async new Task<Procedure?> GetByIdAsync(Guid id, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        return await GetQuery(asNoTracking)
            .Include(x => x.Specialization)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    private IQueryable<Procedure> GetQuery(bool asNoTracking) =>
        asNoTracking ? _procedures.AsNoTracking() : _procedures;
}
