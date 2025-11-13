using Authorization.DAL.Entities;
using Authorization.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Authorization.DAL.Repositories;

public class UserRepository(ApplicationDbContext _db) : IUserRepository
{
    public async Task<User> CreateAsync(User newUser, CancellationToken cancellationToken = default)
    {
        _db.Users.Add(newUser);
        await _db.SaveChangesAsync();
        return newUser;
    }

    public async Task DeleteAsync(User user, CancellationToken cancellationToken = default)
    {
        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
    }

    public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _db.Users
        .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _db.Users
       .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User> UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        _db.Users.Update(user);
        await _db.SaveChangesAsync();

        return user;
    }
}
