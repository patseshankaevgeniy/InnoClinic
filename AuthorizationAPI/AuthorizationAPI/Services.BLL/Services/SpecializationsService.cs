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
            var newSpecialization = await CheckSpecialization(createdModel.Name, cancellationToken: cancellationToken);

            newSpecialization = await specRepository.CreateAsync(mapper.Map<Specialization>(createdModel), cancellationToken: cancellationToken);

            return mapper.Map<SpecializationModel>(newSpecialization);
        }

        public async Task DeleteAsync(Guid specializationId, CancellationToken cancellationToken = default)
        {
            var specialization = await CheckSpecialization(specializationId, cancellationToken: cancellationToken);

            await specRepository.DeleteAsync(specialization, cancellationToken);
        }

        public async Task<SpecializationModel> FindByNameAsync(string specializationName, CancellationToken cancellationToken = default)
        {
            var specialization = await CheckSpecialization(specializationName: specializationName, cancellationToken: cancellationToken);

            return mapper.Map<SpecializationModel>(specialization);
        }

        public async Task<List<SpecializationModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var specializations = await specRepository.GetAllAsync(cancellationToken: cancellationToken);
            return mapper.Map<List<SpecializationModel>>(specializations);
        }

        public async Task<SpecializationModel> GetAsync(Guid specializationId, CancellationToken cancellationToken = default)
        {
            var specialization = await CheckSpecialization(id: specializationId, cancellationToken: cancellationToken);
            return mapper.Map<SpecializationModel>(specialization);
        }

        public async Task<SpecializationModel> UpdateAsync(UpdatedSpecializationModel updatedModel, CancellationToken cancellationToken = default)
        {
            var updatedSpecialization = await CheckSpecialization(updatedModel.Id, cancellationToken: cancellationToken);

            updatedSpecialization.Name = updatedModel.Name;

            updatedSpecialization = await specRepository.UpdateAsync(updatedSpecialization, cancellationToken: cancellationToken);

            return mapper.Map<SpecializationModel>(updatedSpecialization);
        }

        private async Task<Specialization> CheckSpecialization(Guid id, CancellationToken cancellationToken = default)
        {
            var checkedSpecialization = await specRepository.GetByIdAsync(id, cancellationToken);
            if (checkedSpecialization is null)
            {
                throw new InvalidOperationException($"Procedure with id '{id}' does not exist.");
            }
            return checkedSpecialization;
        }

        private async Task<Specialization> CheckSpecialization(string specializationName, CancellationToken cancellationToken = default)
        {
            var checkedSpecialization = await specRepository.FindAsync(specializationName, cancellationToken);
            if (checkedSpecialization is null)
            {
                throw new InvalidOperationException($"Procedure with name '{specializationName}' does not exist.");
            }
            return checkedSpecialization;
        }
    }
}
