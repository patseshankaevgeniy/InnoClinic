using AutoMapper;
using FluentValidation;
using Profiles.BLL.Common.Constants;
using Profiles.BLL.Common.Exceptions;
using Profiles.BLL.Models;
using Profiles.BLL.Models.Doctors;
using Profiles.BLL.Services.Interfaces;
using Profiles.DAL.Entities;
using Profiles.DAL.Models;
using Profiles.DAL.Repositories.Interfaces;
using ValidationException = Profiles.BLL.Common.Exceptions.ValidationException;

namespace Profiles.BLL.Services
{
    public class DoctorsService(
    IGenericRepository<Doctor> doctorRepository,
     IValidator<CreatedDoctorModel> createdDoctorValidator,
    IValidator<UpdatedDoctorModel> updatedDoctorValidator,
    IMapper mapper) : IDoctorsService
    {
        public async Task<DoctorModel> CreateAsync(CreatedDoctorModel createdModel, CancellationToken cancellationToken = default)
        {
            var validationResult = await createdDoctorValidator.ValidateAsync(createdModel, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.ToString());
            }

            var newDoctor = mapper.Map<Doctor>(createdModel);

            newDoctor = await doctorRepository.CreateAsync(newDoctor, cancellationToken: cancellationToken);

            return mapper.Map<DoctorModel>(newDoctor);
        }

        public async Task<DoctorModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var doctorEntity = await doctorRepository.GetByPredicateAsync(x => x.Id == id);

            if (doctorEntity is null)
            {
                throw new NotFoundException(ExceptionConstants.NotFoundDoctor);
            }

            return mapper.Map<DoctorModel>(doctorEntity);
        }

        public async Task<List<DoctorModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var doctors = await doctorRepository.GetAllAsync(cancellationToken: cancellationToken);
            return mapper.Map<List<DoctorModel>>(doctors);
        }

        public async Task<List<DoctorModel>> GetPagedAsync(PaginationParametersModel paginationParameters, CancellationToken cancellationToken = default)
        {
            var doctors = await doctorRepository.GetPagedAsync(mapper.Map<PaginationParameters>(paginationParameters), cancellationToken: cancellationToken);

            return mapper.Map<List<DoctorModel>>(doctors);
        }

        public async Task<DoctorModel> UpdateAsync(UpdatedDoctorModel updatedModel, CancellationToken cancellationToken = default)
        {
            var validationResult = await updatedDoctorValidator.ValidateAsync(updatedModel, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.ToString());
            }

            var updatedDoctor = await doctorRepository.GetByPredicateAsync(x => x.Id == updatedModel.Id);
            if (updatedDoctor is null)
            {
                throw new NotFoundException(ExceptionConstants.NotFoundDoctor);
            }

            updatedDoctor = await doctorRepository.UpdateAsync(mapper.Map(updatedModel, updatedDoctor));

            return mapper.Map<DoctorModel>(updatedDoctor);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var deletedDoctor = await doctorRepository.GetByPredicateAsync(x => x.Id == id, cancellationToken: cancellationToken);

            if (deletedDoctor is null)
            {
                throw new NotFoundException(ExceptionConstants.NotFoundDoctor);
            }

            await doctorRepository.DeleteAsync(deletedDoctor);
        }
    }
}
