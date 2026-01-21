using Authorization.BLL.Models;
using Authorization.DAL.Entities;
using AutoMapper;

namespace Authorization.BLL.Common;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Identity, IdentityModel>()
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.HashPassword));

        CreateMap<IdentityModel, Identity>()
            .ForMember(dest => dest.HashPassword, opt => opt.MapFrom(src => src.Password));
    }
}
