using AutoMapper;
using Services.BLL.Models.Specializations;
using Services.BLL.Services.Interfaces;
using Services.DAL.Entities;
using Services.DAL.Repositories.Interfaces;

namespace Services.BLL.Services
{
    public class SpecializationsService(IMapper mapper, ISpecializationsRepository specRepository) : ISpecializationsService
    {
        public async Task<SpecializationModel> CreateAsync(CreatedSpecializationModel createdModel, CancellationToken cancellationToken = default)
        {
            var newSpecialization = await specRepository.FindAsync(createdModel.Name, cancellationToken: cancellationToken);
            if (newSpecialization is not null)
            {
                throw new InvalidOperationException($"Specialization with name {createdModel.Name} already exists.");
            }

            newSpecialization = await specRepository.CreateAsync(mapper.Map<Specialization>(createdModel), cancellationToken: cancellationToken);

            return mapper.Map<SpecializationModel>(newSpecialization);
        }

        public async Task DeleteAsync(Guid specializationId, CancellationToken cancellationToken = default)
        {
            var specialization = await specRepository.GetAsync(specializationId, cancellationToken: cancellationToken);
            if (specialization is null)
            {
                throw new KeyNotFoundException($"Specialization with Id {specializationId} not found.");
            }

            await specRepository.DeleteAsync(specialization, cancellationToken);
        }

        public async Task<SpecializationModel> FindByNameAsync(string specializationName, CancellationToken cancellationToken = default)
        {
            var specialization = await specRepository.FindAsync(specializationName, cancellationToken: cancellationToken);

            return mapper.Map<SpecializationModel>(specialization);
        }

        public async Task<List<SpecializationModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var specializations = await specRepository.GetAllAsync(cancellationToken: cancellationToken);
            return mapper.Map<List<SpecializationModel>>(specializations);
        }

        public async Task<SpecializationModel> GetAsync(Guid specializationId, CancellationToken cancellationToken = default)
        {
            var specialization = await specRepository.GetAsync( specializationId, cancellationToken: cancellationToken);
            return mapper.Map<SpecializationModel>(specialization);
        }

        public async Task<SpecializationModel> UpdateAsync(UpdatedSpecializationModel updatedModel, CancellationToken cancellationToken = default)
        {
            var updatedSpecialization = await specRepository.GetAsync(updatedModel.Id, cancellationToken: cancellationToken);

            if (updatedSpecialization is null)
            {
                throw new KeyNotFoundException($"Specialization with Id {updatedModel.Id} not found.");
            }

            updatedSpecialization.Name = updatedModel.Name;

            updatedSpecialization = await specRepository.UpdateAsync(updatedSpecialization, cancellationToken: cancellationToken);

            return mapper.Map<SpecializationModel>(updatedSpecialization);
        }
    }
}
