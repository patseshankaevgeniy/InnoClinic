using Authorization.BLL.Models;
using Authorization.BLL.Services.Interfaces;
using Authorization.DAL.Entities;
using Authorization.DAL.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Authorization.BLL.Services;

public sealed class IdenityService(IIdentityRepository identityRepository) : IIdenityService
{
    public async Task<IdentityModel> CreateAsync(IdentityModel identityModel, CancellationToken cancellationToken = default)
    {
        var chekedIdentity = await identityRepository.GetByEmailAsync(identityModel.Email, cancellationToken);
        if (chekedIdentity is not null)
        {
            throw new ValidationException("A user with this email address already exists.");
        }
        var newIdentityModel = new IdentityModel();
       
        if (identityModel.Role is UserRole.Worker)
        {
            var newWorker = await identityRepository.CreateAsync(new Worker
                {
                    Email = identityModel.Email,
                    Password = identityModel.Password,
                    FirstName = identityModel.FirstName,
                    Role = identityModel.Role,
                    Id = Guid.NewGuid(),
                    LastName = identityModel.LastName,
                    Status = WorkerStatus.atWork,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                });

            newIdentityModel.Role = newWorker.Role;
            newIdentityModel.Email = newWorker.Email;
            newIdentityModel.FirstName = newWorker.FirstName;
            newIdentityModel.LastName = newWorker.LastName;
            newIdentityModel.Id = newWorker.Id;
            newIdentityModel.Password = newWorker.Password;

            return newIdentityModel;
         }

        var newPatient = await identityRepository.CreateAsync(new Patient
        {
            Email = identityModel.Email,
            Password = identityModel.Password,
            FirstName = identityModel.FirstName,
            Role = identityModel.Role,
            Id = Guid.NewGuid(),
            LastName = identityModel.LastName,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        });

        newIdentityModel.Role = newPatient.Role;
        newIdentityModel.Email = newPatient.Email;
        newIdentityModel.FirstName = newPatient.FirstName;
        newIdentityModel.LastName = newPatient.LastName;
        newIdentityModel.Id = newPatient.Id;
        newIdentityModel.Password = newPatient.Password;

        return newIdentityModel;
    }

    public async Task<IdentityModel> GetAsync(string email, CancellationToken cancellationToken = default)
    {
        var identity = await identityRepository.GetByEmailAsync(email, cancellationToken);

        return new IdentityModel
        {
            Role = identity.Role,
            Email = identity.Email,
            FirstName = identity.FirstName,
            LastName = identity.LastName,
            Id = identity.Id,
            Password = identity.Password,
            Status = identity is Worker worker ? worker.Status : null
        };
    }

    public async Task<IdentityModel> UpdateAsync(IdentityModel identityModel, CancellationToken cancellationToken = default)
    {
        if (identityModel.Role is UserRole.Worker)
        {
            var updatedWorker = await identityRepository.UpdateAsync(new Worker
            {
                Id = identityModel.Id,
                FirstName = identityModel.FirstName,
                LastName = identityModel.LastName,
                Email = identityModel.Email,
                Password = identityModel.Password,
                Role = identityModel.Role,
                Status = identityModel.Status ?? WorkerStatus.atWork
            }, cancellationToken);

            identityModel.Role = updatedWorker.Role;
            identityModel.Email = updatedWorker.Email;
            identityModel.FirstName = updatedWorker.FirstName;
            identityModel.LastName = updatedWorker.LastName;
            identityModel.Id = updatedWorker.Id;
            identityModel.Password = updatedWorker.Password;

            return identityModel;
        }

        else {
            
               var updatedPatient = await identityRepository.UpdateAsync(new Patient
            {
                Id = identityModel.Id,
                FirstName = identityModel.FirstName,
                LastName = identityModel.LastName,
                Email = identityModel.Email,
                Password = identityModel.Password,
                Role = identityModel.Role,
            }, cancellationToken);

            identityModel.Role = updatedPatient.Role;
            identityModel.Email = updatedPatient.Email;
            identityModel.FirstName = updatedPatient.FirstName;
            identityModel.LastName = updatedPatient.LastName;
            identityModel.Id = updatedPatient.Id;
            identityModel.Password = updatedPatient.Password;
            return identityModel;
        }
    }
}
