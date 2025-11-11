using Authorization.Domain.Entities;

namespace Authorization.Application.Common.Services.Interfaces;

public interface IUserRepository
{
    Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken);
    Task<User> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<User> CreateAsync(User newUser, CancellationToken cancellationToken);
    Task<User> UpdateAsync(User user, CancellationToken cancellationToken);
    Task DeleteAsync(User user, CancellationToken cancellationToken);
}
