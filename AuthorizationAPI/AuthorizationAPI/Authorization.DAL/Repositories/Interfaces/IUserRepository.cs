using Authorization.DAL.Entities;

namespace Authorization.DAL.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User> CreateAsync(User newUser, CancellationToken cancellationToken = default);
    Task<User> UpdateAsync(User newUser, CancellationToken cancellationToken = default);
}
