using Services.DAL.Entities;

namespace Services.DAL.Repositories.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<TEntity> CreateAsync(TEntity createdEntity, CancellationToken cancellationToken);
    Task<TEntity> UpdateAsync(TEntity updatedEntity, CancellationToken cancellationToken);
    Task DeleteAsync(TEntity deletedEntity, CancellationToken cancellationToken);
}
