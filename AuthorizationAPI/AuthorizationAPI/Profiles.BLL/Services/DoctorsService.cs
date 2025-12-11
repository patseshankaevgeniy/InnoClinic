using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Profiles.BLL.Models;
using Profiles.BLL.Models.Doctors;
using Profiles.BLL.Services.Interfaces;
using Profiles.DAL.Entities;
using Profiles.DAL.Models;
using Profiles.DAL.Repositories.Interfaces;

namespace Profiles.BLL.Services
{
    public class DoctorsService(
    IGenericRepository<Doctor> doctorRepository,
    IGenericRepository<Specialization> specRepository,
    IMapper mapper) : IDoctorsService
    {
        public async Task<DoctorModel> CreateAsync(CreatedDoctorModel createdModel, CancellationToken cancellationToken = default)
        {
            var newDoctorSpecialization = await FindSpecialization(createdModel.SpecializationName, cancellationToken);

            var newDoctor = new Doctor
            {
                FirstName = createdModel.FirstName,
                LastName = createdModel.LastName,
                MiddleName = createdModel.MiddleName,
                PhoneNumber = createdModel.PhoneNumber,
                DateOfBirth = createdModel.DateOfBirth,
                Status = createdModel.Status,
                CareerStartAt = createdModel.CareerStartAt,
                SpecializationId = newDoctorSpecialization.Id,
            };

            newDoctor = await doctorRepository.CreateAsync(newDoctor, cancellationToken: cancellationToken);

            return mapper.Map<DoctorModel>(newDoctor);
        }

        public async Task<DoctorModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var doctorEntity = await doctorRepository
                    .GetByPredicateAsync(
                        x => x.Id == id,
                        x => x.Include(d => d.Specialization));

            if (doctorEntity is null)
            {
                throw new Exception();
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
            var doctorSpecialization = await FindSpecialization(updatedModel.SpecializationName, cancellationToken);

            var updatedDoctor = await doctorRepository.GetByPredicateAsync(x => x.Id == updatedModel.Id);
            if (updatedDoctor is null)
            {
                throw new Exception();
            }

            updatedDoctor.FirstName = updatedModel.FirstName;
            updatedDoctor.LastName = updatedModel.LastName;
            updatedDoctor.MiddleName = updatedModel.MiddleName;
            updatedDoctor.PhoneNumber = updatedModel.PhoneNumber;
            updatedDoctor.Status = updatedModel.Status;
            updatedDoctor.Specialization = doctorSpecialization;
            updatedDoctor.SpecializationId = doctorSpecialization.Id;

            updatedDoctor = await doctorRepository.UpdateAsync(updatedDoctor);

            return mapper.Map<DoctorModel>(updatedDoctor);
        }

        public async Task DeleteAsync(Guid doctorId, CancellationToken cancellationToken = default)
        {
            var deletedDoctor = await doctorRepository.GetByPredicateAsync(x => x.Id == doctorId, cancellationToken: cancellationToken);

            if (deletedDoctor is null)
            {
                throw new Exception();
            }

            await doctorRepository.DeleteAsync(deletedDoctor);
        }

        private async Task<Specialization> FindSpecialization(string specializationName, CancellationToken cancellationToken)
        {
            var specialization = await specRepository.GetByPredicateAsync(x => x.Name == specializationName, cancellationToken: cancellationToken);
            if (specialization is null)
            {
                throw new Exception();
            }

            return specialization;
        }
    }
}
