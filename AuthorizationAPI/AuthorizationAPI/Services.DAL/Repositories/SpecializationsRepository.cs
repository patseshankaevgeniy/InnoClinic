using Microsoft.EntityFrameworkCore;
using Services.DAL.Entities;
using Services.DAL.Repositories.Interfaces;

namespace Services.DAL.Repositories;

public class SpecializationsRepository(ServicesDbContext db) : GenericRepository<Specialization>(db), ISpecializationsRepository
{
    private readonly DbSet<Specialization> _specializations = db.Set<Specialization>();

    public async new Task<Specialization?> GetByIdAsync(Guid id, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        return await GetQuery(asNoTracking)
            .Include(x => x.Procedures)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    private IQueryable<Specialization> GetQuery(bool asNoTracking) =>
        asNoTracking ? _specializations.AsNoTracking() : _specializations;
}
