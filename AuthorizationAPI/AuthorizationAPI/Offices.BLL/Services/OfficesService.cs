using AutoMapper;
using Offices.BLL.Models;
using Offices.BLL.Services.Interfaces;
using Offices.DAL.Entities;
using Offices.DAL.Repositories.Interfaces;

namespace Offices.BLL.Services;

public class OfficesService(IOfficeRepository officeRepository, IMapper mapper) : IOfficesService
{
    private bool asNoTracking = true;

    public async Task<OfficeModel> CreateAsync(OfficeModel newOfficeModel, CancellationToken cancellationToken = default)
    {
        var newOfficeEntity = await officeRepository.CreateAsync(new Office
        {
            Id = Guid.NewGuid(),
            IsActive = newOfficeModel.IsActive,
            PhoneNumber = newOfficeModel.PhoneNumber,
            Address = newOfficeModel.Address,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        }, cancellationToken);

        return mapper.Map<OfficeModel>(newOfficeEntity);

    }

    public async Task DeleteAsync(Guid officeId, CancellationToken cancellationToken = default)
    {
        var officeEntity = await officeRepository.GetAsync(officeId, asNoTracking, cancellationToken);

        await officeRepository.DeleteAsync(officeEntity!);
    }

    public async Task<List<OfficeModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var officeEntities = await officeRepository.GetAllAsync(asNoTracking, cancellationToken);

        var officeModels = officeEntities.Select(officeEntity => mapper.Map<OfficeModel>(officeEntity)).ToList();

        return officeModels;
    }

    public async Task<OfficeModel> GetAsync(Guid officeId, CancellationToken cancellationToken = default)
    {
        var officeEntity = await officeRepository.GetAsync(officeId, asNoTracking, cancellationToken);

        return mapper.Map<OfficeModel>(officeEntity);
    }

    public async Task<OfficeModel> UpdateAsync(OfficeModel updatedOfficeModel, CancellationToken cancellationToken = default)
    {
        var updatedOfficeEntity = await officeRepository.GetAsync(updatedOfficeModel.Id, asNoTracking, cancellationToken);

        updatedOfficeEntity!.IsActive = updatedOfficeModel.IsActive;
        updatedOfficeEntity.PhoneNumber = updatedOfficeModel.PhoneNumber;
        updatedOfficeEntity.Address = updatedOfficeModel.Address;
        updatedOfficeEntity.CreatedAt = updatedOfficeModel.CreatedAt;
        updatedOfficeEntity.UpdatedAt = DateTime.UtcNow;

        updatedOfficeEntity = await officeRepository.UpdateAsync(updatedOfficeEntity, cancellationToken);

        return mapper.Map<OfficeModel>(updatedOfficeEntity);
    }
}
