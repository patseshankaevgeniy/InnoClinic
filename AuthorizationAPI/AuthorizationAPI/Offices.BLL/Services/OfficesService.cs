using AutoMapper;
using Offices.BLL.Common;
using Offices.BLL.Common.Exceptions;
using Offices.BLL.Models;
using Offices.BLL.Services.Interfaces;
using Offices.DAL.Entities;
using Offices.DAL.Repositories.Interfaces;

namespace Offices.BLL.Services;

public class OfficesService(
    IOfficeRepository officeRepository, 
    IMapper mapper,
    IGuidService guidService) : IOfficesService
{
    public async Task<OfficeInputModel> CreateAsync(OfficeInputModel newOfficeModel, CancellationToken cancellationToken = default)
    {
        var newOfficeEntity = await officeRepository.CreateAsync(new Office
        {
            Id = guidService.NewGuid(),
            IsActive = newOfficeModel.IsActive,
            PhoneNumber = newOfficeModel.PhoneNumber,
            Address = newOfficeModel.Address,
        }, cancellationToken);

        return mapper.Map<OfficeInputModel>(newOfficeEntity);
    }

    public async Task DeleteAsync(Guid officeId, CancellationToken cancellationToken = default)
    {
        var officeEntity = await officeRepository.GetAsync(officeId, cancellationToken: cancellationToken)
            ?? throw new NotFoundException(ExceptionConstants.NotFoundOffice);

        await officeRepository.DeleteAsync(officeEntity);
    }

    public async Task<List<OfficeResourceModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var officeEntities = await officeRepository.GetAllAsync(cancellationToken: cancellationToken);

        var officeModels = officeEntities.Select(officeEntity => mapper.Map<OfficeResourceModel>(officeEntity)).ToList();

        return officeModels;
    }

    public async Task<OfficeResourceModel> GetAsync(Guid officeId, CancellationToken cancellationToken = default)
    {
        var officeEntity = await officeRepository.GetAsync(officeId, cancellationToken: cancellationToken) 
            ?? throw new NotFoundException(ExceptionConstants.NotFoundOffice);

        return mapper.Map<OfficeResourceModel>(officeEntity);
    }

    public async Task<OfficeInputModel> UpdateAsync(OfficeInputModel updatedOfficeModel, CancellationToken cancellationToken = default)
    {
        var updatedOfficeEntity = await officeRepository.GetAsync(updatedOfficeModel.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException(ExceptionConstants.NotFoundOffice);

        updatedOfficeEntity.Address = updatedOfficeModel.Address;
        updatedOfficeEntity.PhoneNumber = updatedOfficeModel.PhoneNumber;
        updatedOfficeEntity.IsActive = updatedOfficeModel.IsActive;

        updatedOfficeEntity = await officeRepository.UpdateAsync(updatedOfficeEntity, cancellationToken);

        return mapper.Map<OfficeInputModel>(updatedOfficeEntity);
    }
}
