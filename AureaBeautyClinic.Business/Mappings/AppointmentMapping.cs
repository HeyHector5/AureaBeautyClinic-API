using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;

namespace AureaBeautyClinic.Business.Mappings
{
    public static class AppointmentMapping
    {
        public static AppointmentDTO ToDto(this Appointments appointment) => new(
            appointment.AppointmentID,
            appointment.UserID,
            appointment.DoctorID,
            appointment.Scheduled,
            appointment.State,
            appointment.Notes,
            appointment.Created,
            appointment.User.ToDto(),
            appointment.Doctor.ToDto()
        );
    }
}
