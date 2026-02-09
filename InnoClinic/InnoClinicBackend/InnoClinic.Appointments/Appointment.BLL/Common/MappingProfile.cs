using Appointment.BLL.Models.Appointments;
using Appointment.DAL.Entities;
using AutoMapper;

namespace Appointment.BLL.Common;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AppointmentEntity, AppointmentModel>().ReverseMap();
        CreateMap<CreatedAppointmentModel, AppointmentEntity>();
        CreateMap<UpdatedAppointmentModel, AppointmentEntity>();
    }
}
