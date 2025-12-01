using AutoMapper;
using Offices.BLL.Models;
using Offices.DAL.Entities;

namespace Offices.BLL.Common;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Office, OfficeInputModel>().ReverseMap();
        CreateMap<Office, OfficeResourceModel>();
    }
}
