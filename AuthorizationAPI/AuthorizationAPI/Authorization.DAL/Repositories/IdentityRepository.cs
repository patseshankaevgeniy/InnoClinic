using Authorization.DAL.Entities;
using Authorization.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Authorization.DAL.Repositories;

public class IdentityRepository : IIdentityRepository
{
    private readonly ApplicationDbContext _db;
    private readonly DbSet<Identity> _identities;

    public IdentityRepository(ApplicationDbContext db)
    {
        _db = db;
        _identities = db.Set<Identity>();
    }
    public async Task<Identity> CreateAsync(Identity newUser, CancellationToken cancellationToken = default)
    {
        await _identities.AddAsync(newUser);
        await _db.SaveChangesAsync(cancellationToken);
        return newUser;
    }

    public async Task<Identity?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _identities
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<Identity?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _identities
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Identity> UpdateAsync(Identity newUser, CancellationToken cancellationToken = default)
    {
        _identities.Update(newUser);
        await _db.SaveChangesAsync(cancellationToken);

        return newUser;
    }

    public async Task DeleteAsync(Identity identity, CancellationToken cancellationToken = default)
    {
        _identities.Remove(identity);
        await _db.SaveChangesAsync();
    }
}
