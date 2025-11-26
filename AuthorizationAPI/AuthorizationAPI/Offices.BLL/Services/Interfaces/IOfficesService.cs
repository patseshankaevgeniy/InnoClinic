using Offices.BLL.Models;

namespace Offices.BLL.Services.Interfaces;

public interface IOfficesService
{
    Task<List<OfficeModel>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<OfficeModel> GetAsync(Guid officeId, CancellationToken cancellationToken = default);
    Task<OfficeModel> CreateAsync(OfficeModel newOfficeModel, CancellationToken cancellationToken = default);
    Task<OfficeModel> UpdateAsync(OfficeModel updatedOfficeModel, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid officeId, CancellationToken cancellationToken = default);
}
