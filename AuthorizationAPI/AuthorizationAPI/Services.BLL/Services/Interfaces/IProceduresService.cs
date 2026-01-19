using Services.BLL.Models.Procedures;
using Services.BLL.Models.Specializations;

namespace Services.BLL.Services.Interfaces;

public interface IProceduresService
{
    Task<List<ProcedureModel>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ProcedureModel> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ProcedureModel> FindByNameAsync(string procedureName, CancellationToken cancellationToken = default);
    Task<ProcedureModel> CreateAsync(CreatedProcedureModel createdModel, CancellationToken cancellationToken = default);
    Task<ProcedureModel> UpdateAsync(UpdatedProcedureModel updatedModel, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
