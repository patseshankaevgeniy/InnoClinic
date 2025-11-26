using Offices.BLL.Models;

namespace Offices.BLL.Services.Interfaces;

public interface IOfficesService
{
    Task<List<OfficeResourceModel>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<OfficeResourceModel> GetAsync(Guid officeId, CancellationToken cancellationToken = default);
    Task<OfficeInputModel> CreateAsync(OfficeInputModel newOfficeModel, CancellationToken cancellationToken = default);
    Task<OfficeInputModel> UpdateAsync(OfficeInputModel updatedOfficeModel, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid officeId, CancellationToken cancellationToken = default);
}
