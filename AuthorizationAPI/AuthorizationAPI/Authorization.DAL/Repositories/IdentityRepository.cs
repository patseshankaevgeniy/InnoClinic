using Authorization.DAL.Entities;
using Authorization.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Authorization.DAL.Repositories;

public class IdentityRepository(ApplicationDbContext db) : IIdentityRepository
{
    public async Task<Identity> CreateIdentityAsync(Identity newUser, CancellationToken cancellationToken = default)
    {
        switch (newUser)
        {
            case Worker worker:
                db.Workers.Add(worker);
                break;
            case Patient patient:
                db.Patients.Add(patient);
                break;
            default:
                throw new ArgumentException("Неизвестный тип пользователя", nameof(newUser));
        }
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
}
