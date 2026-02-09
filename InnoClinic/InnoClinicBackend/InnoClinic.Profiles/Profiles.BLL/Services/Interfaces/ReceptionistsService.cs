using AutoMapper;
using Profiles.BLL.Common.Constants;
using Profiles.BLL.Common.Exceptions;
using Profiles.BLL.Models.Receptionists;
using Profiles.DAL.Entities;
using Profiles.DAL.Repositories.Interfaces;

namespace Profiles.BLL.Services.Interfaces;

public class ReceptionistsService(IGenericRepository<Receptionist> receptionistsRepository, IMapper mapper) : IReceptionistsService
{
    public async Task<ReceptionistModel> CreateAsync(ReceptionistModel createdModel, CancellationToken cancellationToken = default)
    {
        var newReceptionist = await receptionistsRepository.CreateAsync(mapper.Map<Receptionist>(createdModel), cancellationToken);

        return mapper.Map<ReceptionistModel>(newReceptionist);
    }

    public async Task<ReceptionistModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var reseptionist = await CheckReceptionist(id, cancellationToken);

        return mapper.Map<ReceptionistModel>(reseptionist);
    }

    public async Task<ReceptionistModel> UpdateAsync(ReceptionistModel updatedModel, CancellationToken cancellationToken = default)
    {
        var updatedReceptionist = await CheckReceptionist(updatedModel.Id, cancellationToken);

        updatedReceptionist = mapper.Map(updatedModel, updatedReceptionist);

        updatedReceptionist = await receptionistsRepository.UpdateAsync(updatedReceptionist, cancellationToken);

        return mapper.Map<ReceptionistModel>(updatedReceptionist);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var deletedReceptionist = await CheckReceptionist(id, cancellationToken);

        await receptionistsRepository.DeleteAsync(deletedReceptionist, cancellationToken);
    }

    private async Task<Receptionist> CheckReceptionist(Guid id, CancellationToken cancellationToken = default)
    {
        var receptionist = await receptionistsRepository.GetByPredicateAsync(x => x.Id == id, cancellationToken: cancellationToken);
        if (receptionist is null)
        {
            throw new NotFoundException(ExceptionMessages.NotFoundReceptionist);
        }
        return receptionist;
    }
}
