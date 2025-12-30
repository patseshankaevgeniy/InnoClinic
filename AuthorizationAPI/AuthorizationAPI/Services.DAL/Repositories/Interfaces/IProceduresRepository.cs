using Services.DAL.Entities;

namespace Services.DAL.Repositories.Interfaces;

public interface IProceduresRepository : IGenericRepository<Procedure>
{
    new Task<Procedure?> GetByIdAsync(Guid id, bool asNoTracking = true, CancellationToken cancellationToken = default);
    Task<Procedure?> FindAsync(string procedureName, bool asNoTracking = default, CancellationToken cancellationToken = default);
}
