using Microsoft.EntityFrameworkCore.Query;
using Profiles.DAL.Entities;
using System.Linq.Expressions;

namespace Profiles.DAL.Repositories.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<List<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, CancellationToken cancellationToken = default);
    Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, CancellationToken cancellationToken = default);
    Task<TEntity?> FirstAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, CancellationToken cancellationToken = default);
    Task<TEntity> CreateAsync(TEntity item, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, CancellationToken cancellationToken = default);
    Task<TEntity> UpdateAsync(TEntity item, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity item, CancellationToken cancellationToken = default);
}
