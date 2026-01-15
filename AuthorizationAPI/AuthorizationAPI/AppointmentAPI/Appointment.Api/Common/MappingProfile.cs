using Appointment.Api.Dtos.Appointments;
using Appointment.BLL.Models.Appointments;
using AutoMapper;

namespace Appointment.Api.Common;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Appoint mappings
        CreateMap<AppointmentDto, AppointmentModel>().ReverseMap();
        CreateMap<CreatedAppointmentDto, CreatedAppointmentModel>();
        CreateMap<UpdatedAppointmentDto, UpdatedAppointmentModel>();
    }
}
