using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;

namespace AureaBeautyClinic.Business.Mappings
{
    public static class AppointmentMapping
    {
        public static AppointmentDTO ToDto(this Appointment appointment) => new(
            appointment.AppointmentId,
            appointment.UserId,
            appointment.DoctorId,
            appointment.Scheduled,
            appointment.State,
            appointment.Notes,
            appointment.Created,
            appointment.User.ToDto(),
            appointment.Doctor.ToDto()
        );
    }
}
