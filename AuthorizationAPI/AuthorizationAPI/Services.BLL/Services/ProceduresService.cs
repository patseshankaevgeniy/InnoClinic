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
        var checkedProcedure = await proceduresRepository.FindAsync(createdModel.Name, cancellationToken);
        if (checkedProcedure is not null)
        {
            throw new InvalidOperationException($"Procedure with name '{createdModel.Name}' already exists.");
        }

        var checkedSpecialization = await specializationsService.FindByNameAsync(createdModel.SpecializationName, cancellationToken);
        if (checkedSpecialization is null)
        {
            throw new InvalidOperationException($"Specialization with name '{createdModel.SpecializationName}' does not exist.");
        }

        var newProcedure = mapper.Map<Procedure>(createdModel);
        newProcedure.SpecializationId = checkedSpecialization.Id;

        newProcedure = await proceduresRepository.CreateAsync(newProcedure, cancellationToken);

        return mapper.Map<ProcedureModel>(newProcedure);
    }

    public async Task<ProcedureModel> FindByNameAsync(string procedureName, CancellationToken cancellationToken = default)
    {
        var procedure = await proceduresRepository.FindAsync(procedureName, cancellationToken);
        if (procedure is not null)
        {
            throw new InvalidOperationException($"Procedure with name '{procedureName}' already exists.");
        }

        return mapper.Map<ProcedureModel>(procedure);
    }

    public async Task<List<ProcedureModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var procedures = await proceduresRepository.GetAllAsync(cancellationToken);

        return mapper.Map<List<ProcedureModel>>(procedures);
    }

    public async Task<ProcedureModel> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var procedure = await proceduresRepository.GetAsync(id, cancellationToken);
        if (procedure is null)
        {
            throw new InvalidOperationException($"Procedure with id '{id}' does not exist.");
        }

        return mapper.Map<ProcedureModel>(procedure);
    }

    public async Task<ProcedureModel> UpdateAsync(UpdatedProcedureModel updatedModel, CancellationToken cancellationToken = default)
    {
        var updatedProcedure = await proceduresRepository.GetAsync(updatedModel.Id, cancellationToken);
        if (updatedProcedure is null)
        {
            throw new InvalidOperationException($"Procedure with id '{updatedModel.Id}' does not exist.");
        }
        mapper.Map(updatedModel, updatedProcedure);

        var procedure = await proceduresRepository.UpdateAsync(updatedProcedure, cancellationToken);

        return mapper.Map<ProcedureModel>(procedure);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var deletedProcedure = await proceduresRepository.GetAsync(id, cancellationToken);
        if (deletedProcedure is null)
        {
            throw new InvalidOperationException($"Procedure with id '{id}' does not exist.");
        }

        await proceduresRepository.DeleteAsync(deletedProcedure, cancellationToken);
    }
}
