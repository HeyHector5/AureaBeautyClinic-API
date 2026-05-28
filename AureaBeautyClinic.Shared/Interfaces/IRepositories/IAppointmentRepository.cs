using AureaBeautyClinic.Shared.Entities;

namespace AureaBeautyClinic.Shared.Interfaces.IRepositories
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointments>> GetAllAsync();
        Task<Appointments?> GetByIdAsync(int appointmentId);
        Task<IEnumerable<Appointments>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Appointments>> GetByDoctorIdAsync(int doctorId);
        Task<Appointments> CreateAsync(Appointments appointment);
        Task UpdateAsync(Appointments appointment);
    }
}
