using Microsoft.EntityFrameworkCore;
using Services.DAL.Entities;
using Services.DAL.Repositories.Interfaces;

namespace Services.DAL.Repositories;

public class ProceduresRepository(ServicesDbContext db) : GenericRepository<Procedure>(db), IProceduresRepository
{
    private readonly DbSet<Procedure> _procedures = db.Set<Procedure>();

    public async Task<Procedure?> FindAsync(string procedureName, CancellationToken cancellationToken = default)
    {
        return await _procedures
            .FirstOrDefaultAsync(s => s.Name == procedureName, cancellationToken);
    }

    public async Task<Procedure?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _procedures
            .AsNoTracking()
            .Include(x => x.Specialization)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }
}
