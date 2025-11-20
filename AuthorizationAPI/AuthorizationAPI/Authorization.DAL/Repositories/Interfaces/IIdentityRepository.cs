using Authorization.DAL.Entities;

namespace Authorization.DAL.Repositories.Interfaces;

public interface IIdentityRepository
{
    Task<Identity?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<Identity?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Identity> CreateAsync(Identity newUser, CancellationToken cancellationToken = default);
    Task<Identity> UpdateAsync(Identity newUser, CancellationToken cancellationToken = default);
    Task DeleteAsync(Identity identity, CancellationToken cancellationToken = default);
}
