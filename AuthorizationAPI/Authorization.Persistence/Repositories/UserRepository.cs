using Authorization.Application.Common.Services.Interfaces;
using Authorization.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Persistence.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly IApplicationDbContext _db;
    private readonly DbSet<User> _users;

    public UserRepository(IApplicationDbContext db)
    {
        _db = db;
        _users = _db.Set<User>();
    }
    public async Task<User> CreateAsync(User user)
    {
        _users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public async Task DeleteAsync(User user)
    {
        _users.Remove(user);
        await _db.SaveChangesAsync();
    }

    public async Task<User> FindByEmailAsync(string email)
    {
        return await _users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User> GetAsync(int id)
    {
        return await _users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User> UpdateAsync(User user)
    {
        _users.Update(user);
        await _db.SaveChangesAsync();

        return user;
    }
}
