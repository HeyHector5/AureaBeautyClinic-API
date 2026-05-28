using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;

namespace AureaBeautyClinic.Shared.Interfaces.IServices
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentDTO>> GetAllAsync();
        Task<AppointmentDTO?> GetByIdAsync(int appointmentId);
        Task<IEnumerable<AppointmentDTO>> GetByUserIdAsync(int userId);
        Task<IEnumerable<AppointmentDTO>> GetByDoctorIdAsync(int doctorId);
        Task<AppointmentDTO> CreateAsync(Appointments appointment);
        Task UpdateAsync(Appointments appointment);
    }
}
