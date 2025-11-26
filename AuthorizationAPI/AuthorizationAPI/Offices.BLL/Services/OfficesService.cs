using AutoMapper;
using Offices.BLL.Common;
using Offices.BLL.Common.Exceptions;
using Offices.BLL.Models;
using Offices.BLL.Services.Interfaces;
using Offices.DAL.Entities;
using Offices.DAL.Repositories.Interfaces;

namespace Offices.BLL.Services;

public class OfficesService(IOfficeRepository officeRepository, IMapper mapper) : IOfficesService
{
    public async Task<OfficeInputModel> CreateAsync(OfficeInputModel newOfficeModel, CancellationToken cancellationToken = default)
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

        return mapper.Map<OfficeInputModel>(newOfficeEntity);
    }

    public async Task DeleteAsync(Guid officeId, CancellationToken cancellationToken = default)
    {
        var officeEntity = await officeRepository.GetAsync(officeId, cancellationToken: cancellationToken);

        if (officeEntity is null)
        {
            throw new NotFoundException(ExceptionConstants.NotFoundOffice);
        }

        await officeRepository.DeleteAsync(officeEntity);
    }

    public async Task<List<OfficeResourceModel>> GetAllAsync(bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        var officeEntities = await officeRepository.GetAllAsync(asNoTracking, cancellationToken);

        var officeModels = officeEntities.Select(officeEntity => mapper.Map<OfficeResourceModel>(officeEntity)).ToList();

        return officeModels;
    }

    public async Task<OfficeResourceModel> GetAsync(Guid officeId, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        var officeEntity = await officeRepository.GetAsync(officeId, asNoTracking, cancellationToken);

        if (officeEntity is null)
        {
            throw new NotFoundException(ExceptionConstants.NotFoundOffice);
        }

        return mapper.Map<OfficeResourceModel>(officeEntity);
    }

    public async Task<OfficeInputModel> UpdateAsync(OfficeInputModel updatedOfficeModel, CancellationToken cancellationToken = default)
    {
        var updatedOfficeEntity = await officeRepository.GetAsync(updatedOfficeModel.Id, cancellationToken: cancellationToken);

        if (updatedOfficeEntity is null)
        {
            throw new NotFoundException(ExceptionConstants.NotFoundOffice);
        }

        updatedOfficeEntity = mapper.Map<Office>(updatedOfficeModel);
        updatedOfficeEntity.UpdatedAt = DateTime.UtcNow;

        updatedOfficeEntity = await officeRepository.UpdateAsync(updatedOfficeEntity, cancellationToken);

        return mapper.Map<OfficeInputModel>(updatedOfficeEntity);
    }
}
