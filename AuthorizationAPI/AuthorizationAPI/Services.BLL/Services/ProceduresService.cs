using AutoMapper;
using Services.BLL.Models.Procedures;
using Services.BLL.Services.Interfaces;
using Services.DAL.Entities;
using Services.DAL.Repositories.Interfaces;

namespace Services.BLL.Services;

public class ProceduresService(IProceduresRepository proceduresRepository, ISpecializationsService specializationsService, IMapper mapper) : IProceduresService
{
    public async Task<ProcedureModel> CreateAsync(CreatedProcedureModel createdModel, CancellationToken cancellationToken = default)
    {
        var checkedProcedure = await CheckProcedure(procedureName: createdModel.Name, cancellationToken: cancellationToken);

        var checkedSpecialization = await specializationsService.FindByNameAsync(createdModel.SpecializationName, cancellationToken);

        var newProcedure = mapper.Map<Procedure>(createdModel);
        newProcedure.SpecializationId = checkedSpecialization.Id;

        newProcedure = await proceduresRepository.CreateAsync(newProcedure, cancellationToken);

        return mapper.Map<ProcedureModel>(newProcedure);
    }

    public async Task<ProcedureModel> FindByNameAsync(string procedureName, CancellationToken cancellationToken = default)
    {
        var procedure = await CheckProcedure(procedureName: procedureName, cancellationToken: cancellationToken);

        return mapper.Map<ProcedureModel>(procedure);
    }

    public async Task<List<ProcedureModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var procedures = await proceduresRepository.GetAllAsync(cancellationToken);

        return mapper.Map<List<ProcedureModel>>(procedures);
    }

    public async Task<ProcedureModel> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var procedure = await CheckProcedure(id, cancellationToken: cancellationToken);

        return mapper.Map<ProcedureModel>(procedure);
    }

    public async Task<ProcedureModel> UpdateAsync(UpdatedProcedureModel updatedModel, CancellationToken cancellationToken = default)
    {
        var updatedProcedure = await CheckProcedure(updatedModel.Id, cancellationToken: cancellationToken);

        mapper.Map(updatedModel, updatedProcedure);

        var procedure = await proceduresRepository.UpdateAsync(updatedProcedure, cancellationToken);

        return mapper.Map<ProcedureModel>(procedure);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var deletedProcedure = await CheckProcedure(id, cancellationToken: cancellationToken);

        await proceduresRepository.DeleteAsync(deletedProcedure, cancellationToken);
    }

    private async Task<Procedure> CheckProcedure(Guid id, CancellationToken cancellationToken = default)
    {
        var checkedProcedure = await proceduresRepository.GetByIdAsync(id, cancellationToken);
        if (checkedProcedure is null)
        {
            throw new InvalidOperationException($"Procedure with id '{id}' does not exist.");
        }
        return checkedProcedure;
    }

    private async Task<Procedure> CheckProcedure(string procedureName, CancellationToken cancellationToken = default)
    {
        var checkedProcedure = await proceduresRepository.FindAsync(procedureName, cancellationToken);
        if (checkedProcedure is null)
        {
            throw new InvalidOperationException($"Procedure with name '{procedureName}' does not exist.");
        }
        return checkedProcedure;
    }
}
