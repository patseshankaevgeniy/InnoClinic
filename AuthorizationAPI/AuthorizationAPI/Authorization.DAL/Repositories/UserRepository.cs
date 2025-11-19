using Authorization.DAL.Entities;
using Authorization.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Authorization.DAL.Repositories;

public class UserRepository(ApplicationDbContext db) : IUserRepository
{
    public async Task<User> CreateAsync(User newUser, CancellationToken cancellationToken = default)
    {
        await db.Users.AddAsync(newUser);
        await db.SaveChangesAsync(cancellationToken);
        return newUser;
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await db.Users
        .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await db.Users
       .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<User> UpdateAsync(User newUser, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
