using AutoMapper;
using Offices.API.Dtos;
using Offices.BLL.Models;

namespace Offices.API.Common;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<OfficeResourceDto, OfficeResourceModel>().ReverseMap();
        CreateMap<OfficeInputDto, OfficeInputModel>().ReverseMap();
    }
}
