using Offices.DAL.Entities;

namespace Offices.DAL.Repositories.Interfaces;

public interface IOfficeRepository
{
    Task<List<Office>> GetAllAsync(CancellationToken cancellationToken = default, bool asNoTracking = true);
    Task<Office?> GetAsync(Guid id, CancellationToken cancellationToken = default, bool asNoTracking = false);
    Task<Office> CreateAsync(Office newOffice, CancellationToken cancellationToken = default, bool asNoTracking = false);
    Task<Office> UpdateAsync(Office updatedOffice, CancellationToken cancellationToken = default, bool asNoTracking = false);
    Task DeleteAsync(Office office, CancellationToken cancellationToken = default, bool asNoTracking = false);
}
