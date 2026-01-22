using Profiles.BLL.Models.Receptionists;

namespace Profiles.BLL.Services.Interfaces;

public interface IReceptionistsService
{
    Task<ReceptionistModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ReceptionistModel> CreateAsync(ReceptionistModel createdModel, CancellationToken cancellationToken = default);
    Task<ReceptionistModel> UpdateAsync(ReceptionistModel updatedModel, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
