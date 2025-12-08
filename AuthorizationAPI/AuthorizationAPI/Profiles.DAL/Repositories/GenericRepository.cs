using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Profiles.DAL.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Profiles.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ProfilesDbContext _db;
        private readonly DbSet<TEntity> _entities;

        public GenericRepository(ProfilesDbContext db)
        {
            _db = db;
            _entities = _db.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, CancellationToken cancellationToken = default)
        {
            var query = _entities.AsQueryable();
            if (include is not null)
            {
                query = include.Invoke(query);
            }
            return await query
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
        }

        public async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, CancellationToken cancellationToken = default)
        {
            var query = _entities.AsQueryable();
            query = query.Where(predicate);
            if (include is not null)
            {
                query = include.Invoke(query);
            }
            return await query
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
        }

        public async Task<TEntity?> FirstAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, CancellationToken cancellationToken = default)
        {
            var query = _entities.AsQueryable();
            query = query.Where(predicate);
            if (include is not null)
            {
                query = include.Invoke(query);
            }
            return await query
                    .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<TEntity> CreateAsync(TEntity item, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, CancellationToken cancellationToken = default)
        {
            _entities.Add(item);
            await _db.SaveChangesAsync(cancellationToken);

            return item;
        }

        public async Task<TEntity> UpdateAsync(TEntity item, CancellationToken cancellationToken = default)
        {
            _entities.Update(item);
            await _db.SaveChangesAsync(cancellationToken);

            return item;
        }

        public async Task DeleteAsync(TEntity item, CancellationToken cancellationToken = default)
        {
            _entities.Remove(item);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
