using Services.DAL.Entities;

namespace Services.DAL.Repositories.Interfaces;

public interface ISpecializationsRepository : IGenericRepository<Specialization>
{
    new Task<Specialization?> GetByIdAsync(Guid id, bool asNoTracking = true, CancellationToken cancellationToken = default);
    Task<Specialization?> FindAsync(string specializationName, bool disableTracking = default, CancellationToken cancellationToken = default);
}
