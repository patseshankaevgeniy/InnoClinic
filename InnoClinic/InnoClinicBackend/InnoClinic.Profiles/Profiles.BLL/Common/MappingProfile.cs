using AutoMapper;
using Profiles.BLL.Models;
using Profiles.BLL.Models.Doctors;
using Profiles.BLL.Models.Patients;
using Profiles.BLL.Models.Receptionists;
using Profiles.DAL.Entities;
using Profiles.DAL.Models;

namespace Profiles.BLL.Common;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Doctor mappings
        CreateMap<Doctor, DoctorModel>().ReverseMap();
        CreateMap<Doctor, CreatedDoctorModel>().ReverseMap();
        CreateMap<Doctor, UpdatedDoctorModel>().ReverseMap();

        // Patient mappings
        CreateMap<Patient, PatientModel>().ReverseMap();

        // Receptionist mappings
        CreateMap<Receptionist, ReceptionistModel>().ReverseMap();

        // Pagination mappings
        CreateMap<PaginationParametersModel, PaginationParameters>();
    }
}
