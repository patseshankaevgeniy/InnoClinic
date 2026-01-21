using Authorization.API.Dtos;
using Authorization.BLL.Models;
using AutoMapper;

namespace Authorization.API.Common;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<SignInModel, SignInDto>().ReverseMap();
        CreateMap<SignUpModel, SignUpDto>().ReverseMap();
        CreateMap<AuthResultModel, AuthResultDto>().ReverseMap();

        CreateMap<CreatedIdentityDto, IdentityModel>();
        CreateMap<IdentityModel, IdentityDto>();
    }
}
