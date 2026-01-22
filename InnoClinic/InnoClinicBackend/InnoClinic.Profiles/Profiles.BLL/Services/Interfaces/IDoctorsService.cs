using Profiles.BLL.Models;
using Profiles.BLL.Models.Doctors;

namespace Profiles.BLL.Services.Interfaces;

public interface IDoctorsService
{
    Task<List<DoctorModel>> GetAllAsync(CancellationToken cancellationToken);
    Task<List<DoctorModel>> GetPagedAsync(PaginationParametersModel paginationParameters, CancellationToken cancellationToken);
    Task<DoctorModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<DoctorModel> CreateAsync(CreatedDoctorModel createdModel, CancellationToken cancellationToken);
    Task<DoctorModel> UpdateAsync(UpdatedDoctorModel updatedModel, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
