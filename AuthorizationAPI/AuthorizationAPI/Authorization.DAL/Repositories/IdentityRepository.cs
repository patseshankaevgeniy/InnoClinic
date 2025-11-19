using Authorization.DAL.Entities;
using Authorization.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Authorization.DAL.Repositories;

public class IdentityRepository(ApplicationDbContext db) : IIdentityRepository
{
    public async Task<Identity> CreateAsync(Identity newUser, CancellationToken cancellationToken = default)
    {
        await db.Identities.AddAsync(newUser);
        await db.SaveChangesAsync(cancellationToken);
        return newUser;
    }

    public async Task<Identity?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await db.Identities
        .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<Identity?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await db.Identities
       .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<Identity> UpdateAsync(Identity newUser, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Identity identity, CancellationToken cancellationToken = default)
    {
        db.Identities.Remove(identity);
        await db.SaveChangesAsync();
    }
}
