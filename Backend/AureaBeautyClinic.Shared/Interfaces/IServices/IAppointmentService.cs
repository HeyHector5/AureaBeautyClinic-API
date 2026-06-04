using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;

namespace AureaBeautyClinic.Shared.Interfaces.IServices
{
    public interface IAppointmentervice
    {
        Task<IEnumerable<AppointmentDTO>> GetAllAsync();
        Task<AppointmentDTO?> GetByIdAsync(int AppointmentId);
        Task<IEnumerable<AppointmentDTO>> GetByUserIdAsync(int UserId);
        Task<IEnumerable<AppointmentDTO>> GetByDoctorIdAsync(int DoctorId);
        Task<AppointmentDTO> CreateAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
    }
}
