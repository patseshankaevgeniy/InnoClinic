using Authorization.BLL.Models;

namespace Authorization.BLL.Services.Interfaces;

public interface IIdentityService
{
     Task<IdentityModel> GetAsync(Guid id, CancellationToken cancellationToken = default);
     Task<IdentityModel> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
     Task<IdentityModel> CreateAsync(IdentityModel newIdentity, CancellationToken cancellationToken = default);
     Task<IdentityModel> UpdateAsync(IdentityModel updatedIdentity, CancellationToken cancellationToken = default);
     Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
