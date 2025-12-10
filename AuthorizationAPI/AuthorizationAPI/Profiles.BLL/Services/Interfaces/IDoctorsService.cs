using Profiles.BLL.Models;
using Profiles.BLL.Models.Doctors;

namespace Profiles.BLL.Services.Interfaces;

public interface IDoctorsService
{
    Task<List<DoctorModel>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<DoctorModel>> GetPagedAsync(PaginationParametersModel paginationParameters, CancellationToken cancellationToken = default);
    Task<DoctorModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<DoctorModel> CreateAsync(CreatedDoctorModel createdModel, CancellationToken cancellationToken = default);
    Task<DoctorModel> UpdateAsync(UpdatedDoctorModel updatedModel, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
