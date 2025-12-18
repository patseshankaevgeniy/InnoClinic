using Profiles.BLL.Models.Patients;

namespace Profiles.BLL.Services.Interfaces;

public interface IPatientService
{
    Task<PatientModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PatientModel> CreateAsync(CreatedPatientModel createdModel, CancellationToken cancellationToken = default);
    Task<PatientModel> UpdateAsync(UpdatedPatientModel updatedModel, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
