using AutoMapper;
using Profiles.BLL.Models;
using Profiles.BLL.Models.Doctors;
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

        // Pagination mappings
        CreateMap<PaginationParametersModel, PaginationParameters>();
    }
}
