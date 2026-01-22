using Appointment.DAL.Entities;
using Appointment.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Appointment.DAL.Repositories;

public class GenericRepository<TEntity>(AppointmentsDbContext db) : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _entities = db.Set<TEntity>();

    public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _entities.Add(entity);
        await db.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken, bool asNoTracking = true)
    {
        IQueryable<TEntity> query = _entities;

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        return await query.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _entities.Update(entity);
        await db.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _entities.Remove(entity);
        await db.SaveChangesAsync(cancellationToken);
    }
}
