using Profiles.BLL.Models.Patients;

namespace Profiles.BLL.Services.Interfaces;

public interface IPatientService
{
    Task<PatientModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PatientModel> CreateAsync(PatientModel createdModel, CancellationToken cancellationToken = default);
    Task<PatientModel> UpdateAsync(PatientModel updatedModel, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
