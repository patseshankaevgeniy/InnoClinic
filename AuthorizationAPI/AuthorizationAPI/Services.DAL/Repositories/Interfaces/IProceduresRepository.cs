using Services.DAL.Entities;

namespace Services.DAL.Repositories.Interfaces;

public interface IProceduresRepository
{
    Task<List<Procedure>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Procedure?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Procedure?> FindAsync(string procedureName, CancellationToken cancellationToken = default);
    Task<Procedure> CreateAsync(Procedure newProcedure, CancellationToken cancellationToken = default);
    Task<Procedure> UpdateAsync(Procedure updatedProcedure, CancellationToken cancellationToken = default);
    Task DeleteAsync(Procedure deletedProcedure, CancellationToken cancellationToken = default);
}
