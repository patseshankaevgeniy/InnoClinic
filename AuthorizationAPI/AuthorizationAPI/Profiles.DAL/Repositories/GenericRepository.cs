using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Profiles.DAL.Entities;
using Profiles.DAL.Models;
using Profiles.DAL.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Profiles.DAL.Repositories
{
    public class GenericRepository<TEntity>(ProfilesDbContext db) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbSet<TEntity> _entities = db.Set<TEntity>();

        public async Task<List<TEntity>> GetAllAsync(bool disableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _entities;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, bool disableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _entities;

            if (predicate is not null)
            {
                query = query.Where(predicate);
            }
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            
            return await query.ToListAsync(cancellationToken);
        }

        public Task<List<TEntity>> GetPagedAsync(PaginationParameters paginationParameters, Expression<Func<TEntity, bool>> predicate, bool disableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _entities;

            if (predicate is not null)
            {
                query = query.Where(predicate);
            }
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            query =  query
                       .OrderBy(e => e.Id)
                       .Skip(paginationParameters.Skip)
                       .Take(paginationParameters.PageSize);

            return query.ToListAsync(cancellationToken);
        }

        public async Task<TEntity?> GetByPredicateAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _entities;

            if(include is not null)
            {
                query = include.Invoke(query);
            }
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            return await query
                           .Where(predicate)
                           .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<TEntity> CreateAsync(TEntity item, CancellationToken cancellationToken = default)
        {
            _entities.Add(item);
            await db.SaveChangesAsync(cancellationToken);

            return item;
        }

        public async Task<TEntity> UpdateAsync(TEntity item, CancellationToken cancellationToken = default)
        {
            _entities.Update(item);
            await db.SaveChangesAsync(cancellationToken);

            return item;
        }

        public async Task DeleteAsync(TEntity item, CancellationToken cancellationToken = default)
        {
            _entities.Remove(item);
            await db.SaveChangesAsync(cancellationToken);
        }
    }
}
