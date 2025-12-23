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

    public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _entities
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _entities
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<TEntity> UpdateAsync(TEntity updatedEntity, CancellationToken cancellationToken)
    {
        _entities.Update(updatedEntity);
        await db.SaveChangesAsync(cancellationToken);
        return updatedEntity;
    }
    public async Task DeleteAsync(TEntity deletedEntity, CancellationToken cancellationToken)
    {
        _entities.Remove(deletedEntity);
        await db.SaveChangesAsync(cancellationToken);
    }
}
