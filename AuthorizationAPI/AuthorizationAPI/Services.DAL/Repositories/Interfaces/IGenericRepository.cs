using Services.DAL.Entities;

namespace Services.DAL.Repositories.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : CatalogEntity
{
    Task<TEntity?> GetByIdAsync(Guid id, bool asNoTracking = true, CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetAllAsync(bool asNoTracking = true, CancellationToken cancellationToken = default);
    Task<TEntity?> FindAsync(string name, bool asNoTracking = default, CancellationToken cancellationToken = default);
    Task<TEntity> CreateAsync(TEntity createdEntity, CancellationToken cancellationToken = default);
    Task<TEntity> UpdateAsync(TEntity updatedEntity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity deletedEntity, CancellationToken cancellationToken = default);
}
