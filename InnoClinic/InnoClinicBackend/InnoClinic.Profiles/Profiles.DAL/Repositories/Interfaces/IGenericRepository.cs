using Microsoft.EntityFrameworkCore.Query;
using Profiles.DAL.Entities;
using Profiles.DAL.Models;
using System.Linq.Expressions;

namespace Profiles.DAL.Repositories.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<List<TEntity>> GetAllAsync( CancellationToken cancellationToken, bool disableTracking = true);
    Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken, bool disableTracking = true);
    Task<List<TEntity>> GetPagedAsync(PaginationParameters paginationParameters, CancellationToken cancellationToken, bool disableTracking = true);
    Task<TEntity?> GetByPredicateAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool disableTracking = true);
    Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
}
