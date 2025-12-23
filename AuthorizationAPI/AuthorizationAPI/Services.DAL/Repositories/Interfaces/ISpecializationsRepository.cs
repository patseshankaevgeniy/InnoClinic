using Services.DAL.Entities;

namespace Services.DAL.Repositories.Interfaces;

public interface ISpecializationsRepository
{
    Task<List<Specialization>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Specialization?> GetAsync(Guid specializationId, CancellationToken cancellationToken = default);
    Task<Specialization?> FindAsync(string specializationName, CancellationToken cancellationToken = default);
    Task<Specialization> CreateAsync(Specialization newSpecialization, CancellationToken cancellationToken = default);
    Task<Specialization> UpdateAsync(Specialization updatedSpecialization, CancellationToken cancellationToken = default);
    Task DeleteAsync(Specialization deletedSpecialization, CancellationToken cancellationToken = default);
}
