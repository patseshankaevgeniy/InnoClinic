using AutoMapper;
using Services.Api.Dtos.Procedures;
using Services.Api.Dtos.Specializations;
using Services.BLL.Models.Procedures;
using Services.BLL.Models.Specializations;

namespace Services.Api.Common;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Specialization mappings
        CreateMap<SpecializationModel, SpecializationDto>()
            .ReverseMap();
        CreateMap<CreatedSpecializationDto, CreatedSpecializationModel>()
            .ReverseMap();

        CreateMap<CreatedProcedureDto,CreatedProcedureModel>().ReverseMap();
        CreateMap<ProcedureDto,ProcedureModel>().ReverseMap();

        // Pagination mappings
        //        CreateMap<PaginationParametersDto, PaginationParametersModel>();
    }
}

