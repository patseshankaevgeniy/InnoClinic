using Microsoft.EntityFrameworkCore;
using Services.DAL.Entities;
using Services.DAL.Repositories.Interfaces;

namespace Services.DAL.Repositories;

public class GenericRepository<TEntity>(ServicesDbContext db) : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _entities = db.Set<TEntity>();

    public async Task<TEntity> CreateAsync(TEntity createdEntity, CancellationToken cancellationToken)
    {
        _entities.Add(createdEntity);
        await db.SaveChangesAsync(cancellationToken);

        return createdEntity;
    }

    public async Task<List<TEntity>> GetAllAsync(bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        return await GetQuery(asNoTracking)
            .ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        return await GetQuery(asNoTracking)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<TEntity> UpdateAsync(TEntity updatedEntity, CancellationToken cancellationToken = default)
    {
        _entities.Update(updatedEntity);
        await db.SaveChangesAsync(cancellationToken);
        return updatedEntity;
    }
    public async Task DeleteAsync(TEntity deletedEntity, CancellationToken cancellationToken = default)
    {
        _entities.Remove(deletedEntity);
        await db.SaveChangesAsync(cancellationToken);
    }

    private IQueryable<TEntity> GetQuery(bool asNoTracking) =>
        asNoTracking ? _entities.AsNoTracking() : _entities;
}
