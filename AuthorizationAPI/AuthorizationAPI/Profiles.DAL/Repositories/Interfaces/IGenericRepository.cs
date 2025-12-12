using Microsoft.EntityFrameworkCore.Query;
using Profiles.DAL.Entities;
using Profiles.DAL.Models;
using System.Linq.Expressions;

namespace Profiles.DAL.Repositories.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<List<TEntity>> GetAllAsync(bool disableTracking = true, CancellationToken cancellationToken = default);
    Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, bool disableTracking = true, CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetPagedAsync(PaginationParameters paginationParameters, bool disableTracking = true, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByPredicateAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool disableTracking = true, CancellationToken cancellationToken = default);
    Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}
