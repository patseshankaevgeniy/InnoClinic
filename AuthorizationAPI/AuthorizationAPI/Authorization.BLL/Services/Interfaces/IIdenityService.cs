using Authorization.BLL.Models;

namespace Authorization.BLL.Services.Interfaces;

public interface IIdenityService
{
    Task<IdentityModel> CreateAsync(IdentityModel identityModel, CancellationToken cancellationToken = default);
    Task<IdentityModel> GetAsync(string email, CancellationToken cancellationToken = default);
    Task<IdentityModel> UpdateAsync(IdentityModel identityModel, CancellationToken cancellationToken = default);
}
