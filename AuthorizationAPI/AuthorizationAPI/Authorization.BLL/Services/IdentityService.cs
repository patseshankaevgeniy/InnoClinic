using Authorization.BLL.Common.Models.Exceptions;
using Authorization.BLL.Models;
using Authorization.BLL.Services.Interfaces;
using Authorization.DAL.Entities;
using Authorization.DAL.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Authorization.BLL.Services
{
    public sealed class IdentityService(IIdentityRepository identityRepository) : IIdentityService
    {
        public async Task<IdentityModel> CreateAsync(IdentityModel newIdentityModel, CancellationToken cancellationToken = default)
        {
            var chekedIdentity = await identityRepository.GetByEmailAsync(newIdentityModel.Email);
            if (chekedIdentity is not null)
            {
                throw new ValidationException("A user with this email address already exists.");
            }

            var newIdentity = await identityRepository.CreateAsync(new Identity
            {
                Id = Guid.NewGuid(),
                Email = newIdentityModel.Email,
                HashPassword = newIdentityModel.HashPassword,
                Role = newIdentityModel.Role,
                FirstName = newIdentityModel.FirstName,
                LastName = newIdentityModel.LastName,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            newIdentityModel.Email = newIdentity.Email;
            newIdentityModel.HashPassword = newIdentity.HashPassword;
            newIdentityModel.Role = newIdentityModel.Role;
            newIdentityModel.FirstName = newIdentityModel.FirstName;
            newIdentityModel.LastName = newIdentityModel.LastName;

            return newIdentityModel;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var deletedIdentity = await identityRepository.GetAsync(id);
            if (deletedIdentity is null)
            {
                throw new NotFoundException($"Can't find product with id: {id}");
            }

            await identityRepository.DeleteAsync(deletedIdentity, cancellationToken);
        }

        public async Task<IdentityModel> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var chekedIdentity = await identityRepository.GetAsync(id, cancellationToken);

            return new IdentityModel
            {
                Id = chekedIdentity.Id,
                Email = chekedIdentity.Email,
                FirstName = chekedIdentity.FirstName,
                LastName = chekedIdentity.LastName,
                HashPassword = chekedIdentity.HashPassword,
                Role = chekedIdentity.Role
            };
        }

        public async Task<IdentityModel> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var chekedIdentity = await identityRepository.GetByEmailAsync(email, cancellationToken);

            return new IdentityModel
            {
                Id = chekedIdentity.Id,
                Email = chekedIdentity.Email,
                FirstName = chekedIdentity.FirstName,
                LastName = chekedIdentity.LastName,
                HashPassword = chekedIdentity.HashPassword,
                Role = chekedIdentity.Role
            };
        }

        public async Task<IdentityModel> UpdateAsync(IdentityModel updatedIdentityModel, CancellationToken cancellationToken = default)
        {
            var updatedIdentity = await identityRepository.UpdateAsync(new Identity
            {
                Id = updatedIdentityModel.Id,
                Email = updatedIdentityModel.Email,
                FirstName = updatedIdentityModel.FirstName,
                LastName = updatedIdentityModel.LastName,
                HashPassword = updatedIdentityModel.HashPassword,
                Role = updatedIdentityModel.Role,
                UpdatedAt = DateTime.UtcNow
            }, cancellationToken);

            updatedIdentityModel.FirstName = updatedIdentity.FirstName;
            updatedIdentityModel.LastName = updatedIdentity.LastName;
            updatedIdentityModel.Id = updatedIdentity.Id;
            updatedIdentity.Email = updatedIdentityModel.Email;

            return updatedIdentityModel;
        }
    }
}
