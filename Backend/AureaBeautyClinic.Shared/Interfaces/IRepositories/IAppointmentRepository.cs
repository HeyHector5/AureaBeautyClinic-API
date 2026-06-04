using AureaBeautyClinic.Shared.Entities;

namespace AureaBeautyClinic.Shared.Interfaces.IRepositories
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAllAsync();
        Task<Appointment?> GetByIdAsync(int AppointmentId);
        Task<IEnumerable<Appointment>> GetByUserIdAsync(int UserId);
        Task<IEnumerable<Appointment>> GetByDoctorIdAsync(int DoctorId);
        Task<Appointment> CreateAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
    }
}
