using AutoMapper;
using Documents.API.Dtos;
using Documents.BLL.Models;

namespace Documents.API.Common;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DocumentModel, DocumentDto>().ReverseMap();
    }
}
