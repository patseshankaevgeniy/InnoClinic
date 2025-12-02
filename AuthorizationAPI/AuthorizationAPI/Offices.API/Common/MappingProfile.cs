using AutoMapper;
using Offices.API.Dtos.Office;
using Offices.BLL.Models;

namespace Offices.API.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreatedOfficeDto, OfficeResourceModel>().ReverseMap();
        CreateMap<OfficeDto, OfficeResourceModel>().ReverseMap();
        CreateMap<OfficeDto, OfficeInputModel>().ReverseMap();
        CreateMap<UpdatedOfficeDto, OfficeInputModel>().ReverseMap();
    }
}
