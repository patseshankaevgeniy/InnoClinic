using AutoMapper;
using Grpc.Net.ClientFactory;
using InnoClinic.Contracts.Events;
using InnoClinic.Contracts.Grpc;
using MassTransit;
using Profiles.BLL.Common.Constants;
using Profiles.BLL.Common.Exceptions;
using Profiles.BLL.Models;
using Profiles.BLL.Models.Doctors;
using Profiles.BLL.Services.Interfaces;
using Profiles.DAL.Common;
using Profiles.DAL.Entities;
using Profiles.DAL.Models;
using Profiles.DAL.Repositories.Interfaces;

namespace Profiles.BLL.Services;

public class DoctorsService(
    IGenericRepository<Doctor> doctorRepository,
    GrpcClientFactory grpcClientFactory,
    IPublishEndpoint publishEndpoint,
    IMapper mapper,
    IDbTransactionManager unitOfWork) : IDoctorsService
{
    private readonly EntityChecker.EntityCheckerClient _officeClient =
        grpcClientFactory.CreateClient<EntityChecker.EntityCheckerClient>("OfficeClient");

    private readonly EntityChecker.EntityCheckerClient _specializationClient =
        grpcClientFactory.CreateClient<EntityChecker.EntityCheckerClient>("SpecializationClient");

    public async Task<DoctorModel> CreateAsync(CreatedDoctorModel createdModel, CancellationToken cancellationToken)
    {
        await Task.WhenAll(
            EnsureEntityExistsAsync(_officeClient, createdModel.OfficeId, ExceptionMessages.NotFoundOffice, cancellationToken),
            EnsureEntityExistsAsync(_specializationClient, createdModel.SpecializationId, ExceptionMessages.NotFoundSpecialization, cancellationToken));

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var newDoctor = mapper.Map<Doctor>(createdModel);

            newDoctor = await doctorRepository.CreateAsync(newDoctor, cancellationToken);

            await publishEndpoint.Publish<UserCreatedEvent>(new
                {
                    UserId = newDoctor.Id,
                    Email = createdModel.Email!,
                    Password = createdModel.Password!,
                    Role = "Doctor"
                }, cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken);

            return mapper.Map<DoctorModel>(newDoctor);
        }
        catch
        {
            await unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<DoctorModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var doctorEntity = await doctorRepository.GetByPredicateAsync(x => x.Id == id, cancellationToken);

        if (doctorEntity is null)
        {
            throw new NotFoundException(ExceptionMessages.NotFoundDoctor);
        }

        return mapper.Map<DoctorModel>(doctorEntity);
    }

    public async Task<List<DoctorModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        var doctors = await doctorRepository.GetAllAsync(cancellationToken: cancellationToken);
        return mapper.Map<List<DoctorModel>>(doctors);
    }

    public async Task<List<DoctorModel>> GetPagedAsync(PaginationParametersModel paginationParameters, CancellationToken cancellationToken)
    {
        var doctors = await doctorRepository.GetPagedAsync(mapper.Map<PaginationParameters>(paginationParameters), cancellationToken: cancellationToken);

        return mapper.Map<List<DoctorModel>>(doctors);
    }

    public async Task<DoctorModel> UpdateAsync(UpdatedDoctorModel updatedModel, CancellationToken cancellationToken)
    {
        var updatedDoctor = await doctorRepository.GetByPredicateAsync(x => x.Id == updatedModel.Id, cancellationToken);
        if (updatedDoctor is null)
        {
            throw new NotFoundException(ExceptionMessages.NotFoundDoctor);
        }

        updatedDoctor = await doctorRepository.UpdateAsync(mapper.Map(updatedModel, updatedDoctor), cancellationToken);

        return mapper.Map<DoctorModel>(updatedDoctor);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var deletedDoctor = await doctorRepository.GetByPredicateAsync(x => x.Id == id, cancellationToken: cancellationToken);

        if (deletedDoctor is null)
        {
            throw new NotFoundException(ExceptionMessages.NotFoundDoctor);
        }

        await doctorRepository.DeleteAsync(deletedDoctor, cancellationToken);
    }

    private async Task EnsureEntityExistsAsync(EntityChecker.EntityCheckerClient client, Guid id, string errorMessage, CancellationToken cancellationToken)
    {
        var response = await client.CheckExistsAsync(new CheckRequest { Id = id.ToString() }, cancellationToken: cancellationToken);
        if (!response.Exists) throw new NotFoundException(errorMessage);
    }
}
