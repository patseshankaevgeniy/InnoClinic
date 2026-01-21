using Offices.BLL.Models;

namespace Offices.BLL.Services.Interfaces;

public interface IOfficesService
{
    Task<List<OfficeModel>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<OfficeModel> GetAsync(Guid officeId, CancellationToken cancellationToken = default);
    Task<OfficeModel> CreateAsync(CreatedOfficeModel newOfficeModel, CancellationToken cancellationToken = default);
    Task<OfficeModel> UpdateAsync(UpdatedOfficeModel updatedOfficeModel, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid officeId, CancellationToken cancellationToken = default);
}
