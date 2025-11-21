using Authorization.BLL.Common.Models.Exceptions;
using Authorization.BLL.Constants;
using Authorization.BLL.Models;
using Authorization.BLL.Services.Interfaces;
using Authorization.DAL.Entities;
using Authorization.DAL.Repositories.Interfaces;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Authorization.BLL.Services;

public sealed class IdentityService(IIdentityRepository identityRepository, IMapper mapper) : IIdentityService
{
    public async Task<IdentityModel> CreateAsync(IdentityModel newIdentityModel, CancellationToken cancellationToken = default)
    {
        var chekedIdentity = await identityRepository.GetByEmailAsync(newIdentityModel.Email, cancellationToken);
        if (chekedIdentity is not null)
        {
            throw new ValidationException(ExceptionConstants.UserExists);
        }

        var newIdentity = await identityRepository.CreateAsync(new Identity
        {
            Id = Guid.NewGuid(),
            Email = newIdentityModel.Email,
            HashPassword = newIdentityModel.Password,
            Role = newIdentityModel.Role,
            FirstName = newIdentityModel.FirstName,
            LastName = newIdentityModel.LastName,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        }, cancellationToken);

        return mapper.Map<IdentityModel>(newIdentity);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var deletedIdentity = await identityRepository.GetAsync(id);
        if (deletedIdentity is null)
        {
            throw new NotFoundException(ExceptionConstants.UserExists);
        }

        await identityRepository.DeleteAsync(deletedIdentity, cancellationToken);
    }

    public async Task<IdentityModel> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var chekedIdentity = await identityRepository.GetAsync(id, cancellationToken);

        return mapper.Map<IdentityModel>(chekedIdentity);
    }

    public async Task<IdentityModel> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var chekedIdentity = await identityRepository.GetByEmailAsync(email, cancellationToken);

        return mapper.Map<IdentityModel>(chekedIdentity);
    }

    public async Task<IdentityModel> UpdateAsync(IdentityModel updatedIdentityModel, CancellationToken cancellationToken = default)
    {
        var updatedIdentity = await identityRepository.GetByEmailAsync(updatedIdentityModel.Email, cancellationToken);

        updatedIdentity = await identityRepository.UpdateAsync(mapper.Map<Identity>(updatedIdentity), cancellationToken);

        return mapper.Map<IdentityModel>(updatedIdentity);
    }
}
