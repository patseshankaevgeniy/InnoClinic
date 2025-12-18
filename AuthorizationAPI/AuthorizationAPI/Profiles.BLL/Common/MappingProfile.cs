using AutoMapper;
using Profiles.BLL.Models;
using Profiles.BLL.Models.Doctors;
using Profiles.BLL.Models.Patients;
using Profiles.DAL.Entities;
using Profiles.DAL.Models;

namespace Profiles.BLL.Common;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Doctor mappings
        CreateMap<Doctor, DoctorModel>().ReverseMap();
        CreateMap<Doctor, CreatedDoctorModel>();
        CreateMap<Doctor, UpdatedDoctorModel>().ReverseMap();

        CreateMap<Patient, PatientModel>().ReverseMap();

        // Pagination mappings
        CreateMap<PaginationParametersModel, PaginationParameters>();
    }
}
