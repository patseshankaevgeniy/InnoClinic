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
    public async Task<OfficeModel> CreateAsync(CreatedOfficeModel newOfficeModel, CancellationToken cancellationToken = default)
    {
        var newOfficeEntity = await officeRepository.CreateAsync(new Office
        {
            Id = guidService.NewGuid(),
            IsActive = true,
            PhoneNumber = newOfficeModel.PhoneNumber,
            Address = newOfficeModel.Address,
        }, cancellationToken);

        return mapper.Map<OfficeModel>(newOfficeEntity);
    }

    public async Task DeleteAsync(Guid officeId, CancellationToken cancellationToken = default)
    {
        var officeEntity = await officeRepository.GetAsync(officeId, cancellationToken: cancellationToken)
            ?? throw new NotFoundException(ExceptionConstants.NotFoundOffice);

        await officeRepository.DeleteAsync(officeEntity);
    }

    public async Task<List<OfficeModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var officeEntities = await officeRepository.GetAllAsync(cancellationToken: cancellationToken);

        var officeModels = officeEntities.Select(officeEntity => mapper.Map<OfficeModel>(officeEntity)).ToList();

        return officeModels;
    }

    public async Task<OfficeModel> GetAsync(Guid officeId, CancellationToken cancellationToken = default)
    {
        var officeEntity = await officeRepository.GetAsync(officeId, cancellationToken: cancellationToken) 
            ?? throw new NotFoundException(ExceptionConstants.NotFoundOffice);

        return mapper.Map<OfficeModel>(officeEntity);
    }

    public async Task<OfficeModel> UpdateAsync(UpdatedOfficeModel updatedOfficeModel, CancellationToken cancellationToken = default)
    {
        var updatedOfficeEntity = await officeRepository.GetAsync(updatedOfficeModel.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException(ExceptionConstants.NotFoundOffice);

        updatedOfficeEntity.Address = updatedOfficeModel.Address;
        updatedOfficeEntity.PhoneNumber = updatedOfficeModel.PhoneNumber;
        updatedOfficeEntity.IsActive = updatedOfficeModel.IsActive;

        updatedOfficeEntity = await officeRepository.UpdateAsync(updatedOfficeEntity, cancellationToken);

        return mapper.Map<OfficeModel>(updatedOfficeEntity);
    }
}
