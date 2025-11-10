using Authorization.Application.Common.Services.Interfaces;
using Authorization.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Persistence.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;
    private readonly DbSet<User> _users;

    public UserRepository(ApplicationDbContext db)
    {
        _db = db;
        _users = _db.Set<User>();
    }
    public async Task<User> CreateAsync(User user, CancellationToken cancellationToken)
    {
        _users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public async Task DeleteAsync(User user, CancellationToken cancellationToken)
    {
        _users.Remove(user);
        await _db.SaveChangesAsync();
    }

    public async Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _users
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        _users.Update(user);
        await _db.SaveChangesAsync();

        return user;
    }
}
