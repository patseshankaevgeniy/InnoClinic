using AutoMapper;
using Profiles.API.Dtos;
using Profiles.API.Dtos.Doctors;
using Profiles.BLL.Models;
using Profiles.BLL.Models.Doctors;

namespace Profiles.API.Common;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Doctor mappings
        CreateMap<DoctorModel, DoctorDto>()
            .ForMember(x => x.Status, o => o.MapFrom(d => d.Status.ToString()))
            .ReverseMap();
        CreateMap<CreatedDoctorModel, CreatedDoctorDto>()
            .ForMember(x => x.Status, o => o.MapFrom(d => d.Status.ToString()))
            .ReverseMap();
        CreateMap<UpdatedDoctorModel, UpdatedDoctorDto>()
            .ForMember(x => x.Status, o => o.MapFrom(d => d.Status.ToString()))
            .ReverseMap();

        // Pagination mappings
        CreateMap<PaginationParametersDto, PaginationParametersModel>();
    }
}
