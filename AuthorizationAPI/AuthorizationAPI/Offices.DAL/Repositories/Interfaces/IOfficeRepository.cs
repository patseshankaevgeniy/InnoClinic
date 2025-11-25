using Offices.DAL.Entities;

namespace Offices.DAL.Repositories.Interfaces;

public interface IOfficeRepository
{
    Task<List<Office>> GetAllAsync(bool isNoTracking, CancellationToken cancellationToken = default);
    Task<Office?> GetAsync(Guid id, bool isNoTracking, CancellationToken cancellationToken = default);
    Task<Office> CreateAsync(Office newOffice, CancellationToken cancellationToken = default);
    Task<Office> UpdateAsync(Office updatedOffice, CancellationToken cancellationToken = default);
    Task DeleteAsync(Office office, CancellationToken cancellationToken = default);
}
