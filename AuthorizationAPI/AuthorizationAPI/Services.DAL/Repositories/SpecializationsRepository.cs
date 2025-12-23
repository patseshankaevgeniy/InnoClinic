using Microsoft.EntityFrameworkCore;
using Services.DAL.Entities;
using Services.DAL.Repositories.Interfaces;

namespace Services.DAL.Repositories;

public class SpecializationsRepository(ServicesDbContext db) : GenericRepository<Specialization>(db), ISpecializationsRepository
{
    private readonly ServicesDbContext _db = db;
    private readonly DbSet<Specialization> _specializations = db.Set<Specialization>();

    public async Task<Specialization?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _specializations
            .Include(x => x.Procedures).FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<Specialization?> FindAsync(string specializationName, CancellationToken cancellationToken)
    {
        return await _specializations
            .FirstOrDefaultAsync(s => s.Name == specializationName, cancellationToken);
    }
}
