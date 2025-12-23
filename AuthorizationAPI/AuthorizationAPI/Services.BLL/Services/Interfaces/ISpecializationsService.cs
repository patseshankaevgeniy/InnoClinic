using Services.BLL.Models.Specializations;

namespace Services.BLL.Services.Interfaces;

public interface ISpecializationsService
{
    Task<List<SpecializationModel>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<SpecializationModel> GetAsync(Guid Id, CancellationToken cancellationToken = default);
    Task<SpecializationModel> FindByNameAsync(string specializationName, CancellationToken cancellationToken = default);
    Task<SpecializationModel> CreateAsync(CreatedSpecializationModel createdModel, CancellationToken cancellationToken = default);
    Task<SpecializationModel> UpdateAsync(UpdatedSpecializationModel updatedModel, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid Id, CancellationToken cancellationToken = default);
}
