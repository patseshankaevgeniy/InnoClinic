using Appointment.DAL.Entities;

namespace Appointment.DAL.Repositories.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken, bool asNoTracking = true);

    Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken);

    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);

    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
}
