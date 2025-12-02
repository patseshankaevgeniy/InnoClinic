using AutoMapper;
using Offices.API.Dtos.Office;
using Offices.BLL.Models;

namespace Offices.API.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreatedOfficeDto, CreatedOfficeModel>();
        CreateMap<UpdatedOfficeDto, UpdatedOfficeModel>();
        CreateMap<OfficeDto, OfficeModel>().ReverseMap();
    }
}
