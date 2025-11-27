using Offices.DAL.Entities;

namespace Offices.DAL.Repositories.Interfaces;

public interface IOfficeRepository
{
    Task<List<Office>> GetAllAsync(CancellationToken cancellationToken = default, bool asNoTracking = true);
    Task<Office?> GetAsync(Guid id, CancellationToken cancellationToken = default, bool asNoTracking = default);
    Task<Office> CreateAsync(Office newOffice, CancellationToken cancellationToken = default);
    Task<Office> UpdateAsync(Office updatedOffice, CancellationToken cancellationToken = default);
    Task DeleteAsync(Office office, CancellationToken cancellationToken = default);
}
