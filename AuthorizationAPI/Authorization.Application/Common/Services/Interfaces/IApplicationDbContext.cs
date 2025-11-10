using Authorization.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Application.Common.Services.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }

    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
