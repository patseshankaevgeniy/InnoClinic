using Microsoft.EntityFrameworkCore;
using Offices.DAL.Entities;
using Offices.DAL.Repositories.Interfaces;

namespace Offices.DAL.Repositories;

public class OfficeRepository : IOfficeRepository
{
    private readonly OfficeApplicationDbContext _db;
    private readonly DbSet<Office> _offices;

    public OfficeRepository(OfficeApplicationDbContext db)
    {
        _db = db;
        _offices = db.Set<Office>();
    }

    public async Task<Office> CreateAsync(Office newOffice, CancellationToken cancellationToken = default)
    {
        await _offices.AddAsync(newOffice);
        await _db.SaveChangesAsync(cancellationToken);
        return newOffice;
    }

    public async Task DeleteAsync(Office office, CancellationToken cancellationToken = default)
    {
        _offices.Remove(office);
        await _db.SaveChangesAsync();
    }

    public Task<List<Office>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _offices
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Office?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _offices.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Office> UpdateAsync(Office updatedOffice, CancellationToken cancellationToken = default)
    {
        _offices.Update(updatedOffice);
        await _db.SaveChangesAsync(cancellationToken);

        return updatedOffice;
    }
}
