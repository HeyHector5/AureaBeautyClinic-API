using AureaBeautyClinic.Shared.Entities;

namespace AureaBeautyClinic.Shared.Interfaces.IRepositories
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllAsync();
        Task<Doctor?> GetByIdAsync(int DoctorId);
        Task<Doctor> CreateAsync(Doctor doctor);
        Task UpdateAsync(Doctor doctor);
    }
}
