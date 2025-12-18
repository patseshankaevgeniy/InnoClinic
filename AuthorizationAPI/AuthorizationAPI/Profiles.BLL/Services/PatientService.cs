using AutoMapper;
using Profiles.BLL.Common.Constants;
using Profiles.BLL.Common.Exceptions;
using Profiles.BLL.Models.Patients;
using Profiles.BLL.Services.Interfaces;
using Profiles.DAL.Entities;
using Profiles.DAL.Repositories.Interfaces;

namespace Profiles.BLL.Services;

public class PatientService(IGenericRepository<Patient> patientRepository, IMapper mapper) : IPatientService
{
    public async Task<PatientModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var patient = await patientRepository.GetByPredicateAsync(x => x.Id == id, cancellationToken: cancellationToken);
        if (patient is null)
        {
            throw new NotFoundException(ExceptionConstants.NotFoundPatient);
        }

        return mapper.Map<PatientModel>(patient);
    }

    public async Task<PatientModel> CreateAsync(CreatedPatientModel createdModel, CancellationToken cancellationToken = default)
    {
        var patientEntity = await patientRepository.CreateAsync(mapper.Map<Patient>(createdModel), cancellationToken);

        return mapper.Map<PatientModel>(patientEntity);
    }

    public async Task<PatientModel> UpdateAsync(UpdatedPatientModel updatedModel, CancellationToken cancellationToken = default)
    {
        var patient = await patientRepository.GetByPredicateAsync(x => x.Id == updatedModel.Id, cancellationToken: cancellationToken);
        if (patient is null)
        {
            throw new NotFoundException(ExceptionConstants.NotFoundPatient);
        }

        patient = mapper.Map(updatedModel, patient);
        var updatedPatient = await patientRepository.UpdateAsync(patient, cancellationToken);

        return mapper.Map<PatientModel>(updatedPatient);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var deletedPatient = await patientRepository.GetByPredicateAsync(x => x.Id == id, cancellationToken: cancellationToken);
        if (deletedPatient is null)
        {
            throw new NotFoundException(ExceptionConstants.NotFoundPatient);
        }

        await patientRepository.DeleteAsync(deletedPatient, cancellationToken);
    }
}
