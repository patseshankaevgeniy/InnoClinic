using Offices.BLL.Models;

namespace Offices.BLL.Services.Interfaces;

public interface IOfficesService
{
    Task<List<OfficeInputModel>> GetAllAsync(bool asNoTracking = true, CancellationToken cancellationToken = default);
    Task<OfficeInputModel> GetAsync(Guid officeId, bool asNoTracking = false, CancellationToken cancellationToken = default);
    Task<OfficeInputModel> CreateAsync(OfficeInputModel newOfficeModel, CancellationToken cancellationToken = default);
    Task<OfficeInputModel> UpdateAsync(OfficeInputModel updatedOfficeModel, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid officeId, CancellationToken cancellationToken = default);
}
