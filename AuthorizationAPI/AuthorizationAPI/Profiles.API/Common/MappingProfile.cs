using AutoMapper;
using Profiles.API.Dtos;
using Profiles.API.Dtos.Doctors;
using Profiles.API.Dtos.Patients;
using Profiles.BLL.Models;
using Profiles.BLL.Models.Doctors;
using Profiles.BLL.Models.Patients;

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

        // Patient mappings
        CreateMap<PatientDto, PatientModel>().ReverseMap();

        // Pagination mappings
        CreateMap<PaginationParametersDto, PaginationParametersModel>();
    }
}
