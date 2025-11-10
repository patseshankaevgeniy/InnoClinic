using Authorization.Domain.Entities;

namespace Authorization.Application.Common.Services.Interfaces;

public interface IUserRepository
{
    Task<User> FindByEmailAsync(string email);
    Task<User> GetAsync(int id);
    Task<User> CreateAsync(User newUser);
    Task<User> UpdateAsync(User user);
    Task DeleteAsync(User user);
}
