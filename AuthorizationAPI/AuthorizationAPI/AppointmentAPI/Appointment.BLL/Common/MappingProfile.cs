using Appointment.BLL.Models.Appointments;
using Appointment.DAL.Entities;
using AutoMapper;

namespace Appointment.BLL.Common;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Appointment mappings
        CreateMap<AppointmentEntity, AppointmentModel>().ReverseMap();
        CreateMap<CreatedAppointmentModel, AppointmentEntity>();
        CreateMap<UpdatedAppointmentModel, AppointmentEntity>();
    }
}
