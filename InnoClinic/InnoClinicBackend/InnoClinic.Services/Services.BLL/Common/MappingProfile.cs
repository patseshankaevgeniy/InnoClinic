using AutoMapper;
using Services.BLL.Models.Procedures;
using Services.BLL.Models.Specializations;
using Services.DAL.Entities;

namespace Services.BLL.Common;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreatedProcedureModel, Procedure>();
        CreateMap<UpdatedProcedureModel, Procedure>();
        CreateMap<Procedure, ProcedureModel>()
            .ForMember(p => p.SpecializationName, x => x.MapFrom(s => s.Specialization.Name))
            .ReverseMap();

        CreateMap<Specialization, SpecializationModel>()
            .ReverseMap();
        CreateMap<CreatedSpecializationModel, Specialization>().ReverseMap();
        CreateMap<UpdatedSpecializationModel, Specialization>().ReverseMap();
    }
}
