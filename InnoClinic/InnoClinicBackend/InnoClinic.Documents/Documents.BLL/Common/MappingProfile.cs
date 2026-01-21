using AutoMapper;
using Documents.BLL.Models;
using Documents.DAL.Entities;

namespace Documents.BLL.Common;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Document, DocumentModel>().ReverseMap();
    }
}
